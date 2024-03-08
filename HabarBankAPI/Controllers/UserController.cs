using AutoMapper;
using HabarBankAPI.Application.DTO;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.AccountLevel;
using HabarBankAPI.Domain.Entities.User;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HabarBankAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> _users_repository;
        private readonly IGenericRepository<AccountLevel> _levels_repository;
            
        private readonly UserService _service;
        private readonly Mapper _mapper;

        public UserController()
        {
            ApplicationDbContext context = new();

            this._mapper = AbstractMapper<UserDTO, User>.MapperB;

            this._users_repository = new GenericRepository<User>(context);

            this._levels_repository = new GenericRepository<AccountLevel>(context);

            this._service = new UserService(_mapper, _users_repository, _levels_repository);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> TakeUsers(int count)
        {
            IList<UserDTO> userDTOs = await _service.GetAccountsList(count);

            return Ok(userDTOs);
        }

        [HttpGet("auth")]
        public async Task<ActionResult<UserDTO>> GetUserByLoginAndPassword(string login, string password)
        {
            UserDTO userDTO = await _service.GetAuthTokenByData(login, password);

            return userDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(long id)
        {
            UserDTO userDTOs = await _service.GetAccountById(id);

            return userDTOs;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddNewUser([FromBody] UserDTO userDTO)
        {
            await this._service.CreateUserAccount(userDTO);

            long userId = (await this._service.GetAccountsList(int.MaxValue)).Max(x => x.AccountId);

            UserDTO user = await this._service.GetAccountById(userId);

            return user;
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(long id, [FromBody] StatusDTO userStatusDTO)
        {
            await this._service.EditAccountStatus(id, null);

            await this._service.EditAccountEnabled(id, userStatusDTO.UserEnabled);

            return NoContent();
        }

        [HttpPut("{id}/profile")]
        public async Task<ActionResult> EditUserProfile(long id, [FromBody] ProfileDTO userProfileDTO)
        {
            await this._service.EditAccountProfile(id, userProfileDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserById(long id)
        {
            await this._service.EditAccountEnabled(id, false);

            return NoContent();
        }
    }
}
