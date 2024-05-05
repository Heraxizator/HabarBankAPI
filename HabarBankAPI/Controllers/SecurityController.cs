using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.AccountLevels;
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
using System.Security.Permissions;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/{version:apiVersion}/security")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _service;

        public SecurityController()
        {
            this._service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet("email")]
        public async Task<ActionResult<string>> GetToken(string email)
        {
            try
            {
                string token = await this._service.GetToken(email);

                return Ok(token);
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
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
                return Ok(ex.Message);
            }
        }
    }
}
