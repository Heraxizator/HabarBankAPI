using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/security")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly SecurityService _service;

        public SecurityController()
        {
            this._service = new SecurityService();
        }

        [HttpGet("email")]
        public async Task<ActionResult<IList<AccountLevelDTO>>> GetToken(string email)
        {
            try
            {
                string token = await this._service.GetToken(email);

                return Ok(token);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> GenerateToken([FromBody] string email)
        {
            try
            {
                await this._service.GenerateToken(email);

                return Ok("Токен создан успешно");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
