using AutoMapper;
using HabarBankAPI.Application.DTO;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain.Entities.User;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HabarBankAPI.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGenericRepository<Admin> _admins_repository;

        private readonly AdminService _service;
        private readonly Mapper _mapper;

        public AdminController()
        {
            ApplicationDbContext context = new();

            this._mapper = AbstractMapper<UserDTO, User>.MapperB;

            this._admins_repository = new GenericRepository<Admin>(context);

            this._service = new AdminService(_mapper, _admins_repository);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDTO>>> TakeUsers(int count)
        {
            IList<AdminDTO> adminDTOs = await _service.GetAccountsList(count);

            return Ok(adminDTOs);
        }

        [HttpGet("auth")]
        public async Task<ActionResult<AdminDTO>> GetAdminByLoginAndPassword(string login, string password)
        {
            AdminDTO adminDTO = await _service.GetAuthTokenByData(login, password);

            return adminDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDTO>> GetAdminById(long id)
        {
            AdminDTO adminDTO = await _service.GetAccountById(id);

            return adminDTO;
        }

        [HttpPost]
        public async Task<ActionResult<AdminDTO>> AddNewAdmin([FromBody] AdminDTO adminDTO)
        {
            await this._service.CreateAdminAccount(adminDTO);

            long adminId = (await this._service.GetAccountsList(int.MaxValue)).Max(x => x.AccountId);

            AdminDTO admin = await this._service.GetAccountById(adminId);

            return admin;
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> EditUserStatus(long id, bool enabled)
        {
            await this._service.EditAccountEnabled(id, enabled);

            return NoContent();
        }

        [HttpPut("{id}/profile")]
        public async Task<ActionResult> EditUserProfile(long id, [FromBody] ProfileDTO userProfileDTO)
        {
            await this._service.EditAccountProfile(id, userProfileDTO);

            return NoContent();
        }
    }
}
