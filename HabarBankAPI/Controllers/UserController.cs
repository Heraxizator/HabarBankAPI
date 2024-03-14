using AutoMapper;
using HabarBankAPI.Application.DTO;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HabarBankAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GenericRepository<User> _users_repository;
        private readonly GenericRepository<UserLevel> _levels_repository;
            
        private readonly UserService _user_service;
        private readonly UserLevelService _userlevel_service;

        private readonly Mapper _mapper;
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public UserController()
        {
            ApplicationDbContext context = new();

            this._mapper = AbstractMapper<UserDTO, User>.MapperB;

            this._mapperA = AbstractMapper<AccountLevelDTO, UserLevel>.MapperB;

            this._mapperB = AbstractMapper<AccountLevelDTO, UserLevel>.MapperA;

            this._users_repository = new GenericRepository<User>(context);

            this._levels_repository = new GenericRepository<UserLevel>(context);

            UnitOfWork unitOfWork = new(context);

            this._user_service = new UserService(_mapper, _users_repository, _levels_repository, unitOfWork);

            this._userlevel_service = new UserLevelService(_levels_repository, unitOfWork, _mapperA, _mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> TakeUsers(int count)
        {
            IList<UserDTO> userDTOs = await _user_service.GetAccountsList(count);

            return Ok(userDTOs);
        }

        [HttpGet("auth")]
        public async Task<ActionResult<UserDTO>> GetUserByLoginAndPassword(string login, string password)
        {
            UserDTO userDTO = await _user_service.GetAuthTokenByData(login, password);

            return userDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(long id)
        {
            UserDTO userDTOs = await _user_service.GetAccountById(id);

            return userDTOs;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddNewUser([FromBody] UserDTO userDTO)
        {
            await this._user_service.CreateUserAccount(userDTO);

            long userId = (await this._user_service.GetAccountsList(int.MaxValue)).Max(x => x.UserId);

            UserDTO user = await this._user_service.GetAccountById(userId);

            return user;
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(long id, long levelId)
        {
            await this._user_service.EditAccountStatus(id, levelId);

            return NoContent();
        }

        [HttpPut("{id}/enabled")]
        public async Task<ActionResult> EditUserEnabled(long id, bool enabled)
        {
            await this._user_service.EditAccountEnabled(id, enabled);

            return NoContent();
        }

        [HttpPut("{id}/profile")]
        public async Task<ActionResult> EditUserProfile(long id, [FromBody] ProfileDTO userProfileDTO)
        {
            await this._user_service.EditAccountProfile(id, userProfileDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserById(long id)
        {
            await this._user_service.EditAccountEnabled(id, false);

            return NoContent();
        }
    }
}
