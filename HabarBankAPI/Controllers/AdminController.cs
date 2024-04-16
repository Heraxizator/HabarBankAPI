using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Admins;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HabarBankAPI.Controllers
{
    [Route("api/{version:apiVersion}/admins")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGenericRepository<Admin> _admins_repository;

        private readonly IAdminService _admin_service;
        private readonly ISecurityService _security_service;

        private readonly Mapper _mapper;

        public AdminController()
        {
            ApplicationDbContext context = new();

            this._mapper = AbstractMapper<AdminDTO, Admin>.MapperB;

            this._admins_repository = new GenericRepository<Admin>(context);

            AppUnitOfWork unitOfWork = new(context);

            this._admin_service = new AdminService(_mapper, _admins_repository, unitOfWork);

            this._security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDTO>>> TakeUsers(int count, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<AdminDTO> adminDTOs = await _admin_service.GetAccountsList(count);

                return Ok(adminDTOs);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("auth")]
        public async Task<ActionResult<AdminDTO>> GetAdminByLoginAndPassword(string login, string password, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                AdminDTO adminDTO = await _admin_service.GetAuthTokenByData(login, password);

                return adminDTO;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDTO>> GetAdminById(long id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                AdminDTO adminDTO = await _admin_service.GetAccountById(id);

                return adminDTO;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdminDTO>> AddNewAdmin([FromBody] AdminDTO adminDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._admin_service.CreateAdminAccount(adminDTO);

                long adminId = (await this._admin_service.GetAccountsList(int.MaxValue)).Max(x => x.AdminId);

                AdminDTO admin = await this._admin_service.GetAccountById(adminId);

                return admin;
            }
           
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(long id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._admin_service.EditAccountEnabled(id, enabled);

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

                await this._admin_service.EditAccountProfile(id, userProfileDTO);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
