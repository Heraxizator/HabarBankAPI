using Asp.Versioning;
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
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HabarBankAPI.Controllers
{
    [Route("api/{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly GenericRepository<User> _users_repository;
        private readonly GenericRepository<UserLevel> _levels_repository;
            
        private readonly UserService _user_service;
        private readonly UserLevelService _userlevel_service;
        private readonly SecurityService _security_service;

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

            AppUnitOfWork appUnitOfWork = new(context);

            this._user_service = new UserService(_mapper, _users_repository, _levels_repository, appUnitOfWork);

            this._userlevel_service = new UserLevelService(_levels_repository, appUnitOfWork, _mapperA, _mapperB);

            SecurityDbContext securityDbContext = new();

            SecurityUnitOfWork securityUnitOfWork = new(securityDbContext);

            GenericRepository<Security> repository = new(securityDbContext);

            this._security_service = new SecurityService(repository, securityUnitOfWork);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> TakeUsers(int count, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<UserDTO> userDTOs = await _user_service.GetAccountsList(count);

                return Ok(userDTOs);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("auth")]
        public async Task<ActionResult<UserDTO>> GetUserByLoginAndPassword(string login, string password, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                UserDTO userDTO = await _user_service.GetAuthTokenByData(login, password);

                return userDTO;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(long id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                UserDTO userDTOs = await _user_service.GetAccountById(id);

                return userDTOs;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddNewUser([FromBody] UserDTO userDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._user_service.CreateUserAccount(userDTO);

                long userId = (await this._user_service.GetAccountsList(int.MaxValue)).Max(x => x.UserId);

                UserDTO user = await this._user_service.GetAccountById(userId);

                return user;
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(long id, long levelId, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._user_service.EditAccountStatus(id, levelId);

                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/enabled")]
        public async Task<ActionResult> EditUserEnabled(long id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._user_service.EditAccountEnabled(id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/profile")]
        public async Task<ActionResult> EditUserProfile(long id, [FromBody] ProfileDTO userProfileDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._user_service.EditAccountProfile(id, userProfileDTO);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveUserById(long id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._user_service.EditAccountEnabled(id, false);

                return NoContent();
            }
           
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
