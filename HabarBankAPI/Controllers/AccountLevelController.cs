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
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/account-levels")]
    [ApiController]
    public class AccountLevelController : ControllerBase
    {
        private readonly AccountLevelService _service;

        public AccountLevelController() 
        {
            Mapper mapperA = AbstractMapper<AccountLevelDTO, AccountLevel>.MapperA;

            Mapper mapperB = AbstractMapper<AccountLevelDTO, AccountLevel>.MapperB;

            GenericRepository<AccountLevel> repository = new(new ApplicationDbContext());

            this._service = new AccountLevelService(repository, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IList<AccountLevelDTO>>> GetAllAccountLevels()
        {
            IList<AccountLevelDTO> dTOs = await this._service.GetAllAccountLevels();

            return Ok(dTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountLevelDTO>> GetAccountLevelById(long id)
        {
            AccountLevelDTO accountLevelDTO = await this._service.GetAccountLevelById(id);

            return accountLevelDTO;
        }

        [HttpPost]
        public async Task<ActionResult<AccountLevelDTO>> AddNewAccountLevel([FromBody] AccountLevelDTO accountLevelDTO)
        {
            await this._service.CreateNewAccountLevel(accountLevelDTO);

            long accountLevelId = (await this._service.GetAllAccountLevels()).Max(x => x.AccountLevelId);

            AccountLevelDTO accountLevel = await this._service.GetAccountLevelById(accountLevelId);

            return accountLevel;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAccountLevelEnabled(long id, bool enabled)
        {
            await this._service.SetAccountLevelEnabled(id, enabled);

            return NoContent();
        }

    }
}
