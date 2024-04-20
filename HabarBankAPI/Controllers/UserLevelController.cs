using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/{version:apiVersion}/account-levels")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserLevelController : ControllerBase
    {
        private readonly IUserLevelService _userlevel_service;
        private readonly ISecurityService _security_service;

        public UserLevelController() 
        {
            Mapper mapperA = AbstractMapper<AccountLevelDTO, UserLevel>.MapperA;

            Mapper mapperB = AbstractMapper<AccountLevelDTO, UserLevel>.MapperB;

            ApplicationDbContext context = new();

            GenericRepository<UserLevel> repository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            this._userlevel_service = new UserLevelService(repository, unitOfWork, mapperA, mapperB);

            this._security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet]
        public async Task<ActionResult<IList<AccountLevelDTO>>> GetAllAccountLevels([FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<AccountLevelDTO> dTOs = await this._userlevel_service.GetAllAccountLevels();

                return Ok(dTOs);
            }
            
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountLevelDTO>> GetAccountLevelById(long id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                AccountLevelDTO accountLevelDTO = await this._userlevel_service.GetAccountLevelById(id);

                return accountLevelDTO;
            }
            
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountLevelDTO>> AddNewAccountLevel([FromBody] AccountLevelDTO accountLevelDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._userlevel_service.CreateNewAccountLevel(accountLevelDTO);

                long accountLevelId = (await this._userlevel_service.GetAllAccountLevels()).Max(x => x.AccountLevelId);

                AccountLevelDTO accountLevel = await this._userlevel_service.GetAccountLevelById(accountLevelId);

                return accountLevel;
            }
            
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAccountLevelEnabled(long id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._userlevel_service.SetAccountLevelEnabled(id, enabled);

                return NoContent();
            }
            
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

    }
}
