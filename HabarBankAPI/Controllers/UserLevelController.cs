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
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/account-levels")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserLevelController : ControllerBase
    {
        private readonly UserLevelService _service;

        public UserLevelController() 
        {
            Mapper mapperA = AbstractMapper<AccountLevelDTO, UserLevel>.MapperA;

            Mapper mapperB = AbstractMapper<AccountLevelDTO, UserLevel>.MapperB;

            ApplicationDbContext context = new();

            GenericRepository<UserLevel> repository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            this._service = new UserLevelService(repository, unitOfWork, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IList<AccountLevelDTO>>> GetAllAccountLevels()
        {
            try
            {
                IList<AccountLevelDTO> dTOs = await this._service.GetAllAccountLevels();

                return Ok(dTOs);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountLevelDTO>> GetAccountLevelById(long id)
        {
            try
            {
                AccountLevelDTO accountLevelDTO = await this._service.GetAccountLevelById(id);

                return accountLevelDTO;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AccountLevelDTO>> AddNewAccountLevel([FromBody] AccountLevelDTO accountLevelDTO)
        {
            try
            {
                await this._service.CreateNewAccountLevel(accountLevelDTO);

                long accountLevelId = (await this._service.GetAllAccountLevels()).Max(x => x.AccountLevelId);

                AccountLevelDTO accountLevel = await this._service.GetAccountLevelById(accountLevelId);

                return accountLevel;
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAccountLevelEnabled(long id, bool enabled)
        {
            try
            {
                await this._service.SetAccountLevelEnabled(id, enabled);

                return NoContent();
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
