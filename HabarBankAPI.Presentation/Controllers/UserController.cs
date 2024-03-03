using AutoMapper;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
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
        private readonly IGenericRepository<Account> _accounts_repository;
        private readonly IGenericRepository<AccountLevel> _levels_repository;
            
        private readonly UserService _service;
        private readonly Mapper _mapper;

        public UserController()
        {
            ApplicationDbContext context = new();

            this._mapper = AbstractMapper<UserDTO, Account>.MapperB;

            this._accounts_repository = new GenericRepository<Account>(context);

            this._levels_repository = new GenericRepository<AccountLevel>(context);

            this._service = new UserService(_mapper, _accounts_repository, _levels_repository);
        }

        [HttpGet("count")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> TakeUsers(int count)
        {
            IList<UserDTO> userDTOs = await _service.GetAccountsList(count);

            return Ok(userDTOs);
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUserByLoginAndPassword(string login, string password)
        {
            UserDTO userDTOs = await _service.GetAuthTokenByData(login, password);

            return userDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            UserDTO userDTOs = await _service.GetAccountById(id);

            return userDTOs;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddNewUser([FromBody] UserDTO userDTO)
        {
            await this._service.CreateAccount(userDTO);

            int userId = (await this._service.GetAccountsList(int.MaxValue)).Max(x => x.AccountId);

            UserDTO user = await this._service.GetAccountById(userId);

            return user;
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(int id, [FromBody] StatusDTO userStatusDTO)
        {
            await this._service.EditAccountStatus(id, null);

            await this._service.EditAccountEnabled(id, userStatusDTO.UserEnabled);

            return NoContent();
        }

        [HttpPut("{id}/profile")]
        public async Task<ActionResult> EditUserProfile(int id, [FromBody] ProfileDTO userProfileDTO)
        {
            await this._service.EditAccountProfile(id, userProfileDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserById(int id)
        {
            await this._service.EditAccountEnabled(id, false);

            return NoContent();
        }
    }
}
