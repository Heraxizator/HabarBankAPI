using AutoMapper;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Application.DTO.Admins;

namespace HabarBankAPI.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly Mapper _mapper;
        private readonly IGenericRepository<Admin> _admins_repository;
        private readonly UnitOfWork _unitOfWork;

        public AdminService(Mapper mapper,
            IGenericRepository<Admin> adminsRepository,
            UnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._admins_repository = adminsRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateAdminAccount(AdminDTO adminDTO)
        {
            AdminFactory adminFactory = new();

            Admin admin = adminFactory
                .WithLogin(adminDTO.AccountLogin)
                .WithPassword(adminDTO.AccountPassword)
                .WithPhone(adminDTO.AccountPhone)
                .WithName(adminDTO.AccountName)
                .WithSurname(adminDTO.AccountSurname)
                .WithPatronymic(adminDTO.AccountPatronymic)
            .Build();

            IList<Admin> admins = await Task.Run(() => this._admins_repository.Get(
                    admin => admin.AccountLogin == adminDTO.AccountLogin && admin.AccountPassword == adminDTO.AccountPassword && admin.Enabled is true).ToList());

            if (admins.Any())
            {
                throw new AccountAlreadyExistsException("Аккаунт с заданным логином и паролём уже существует");
            }

            await Task.Run(
                () => _admins_repository.Create(admin));

            await this._unitOfWork.Commit();
        }

        public async Task EditAccountEnabled(long id, bool enabled)
        {
            Admin? admin = this._admins_repository.Get(admin => admin.AdminId == id && admin.Enabled is true).FirstOrDefault();

            if (admin is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            admin.SetEnabled(enabled);

            await Task.Run(
                () => this._admins_repository.Update(admin));

            await this._unitOfWork.Commit();
        }

        public async Task EditAccountProfile(long id, ProfileDTO profileDTO)
        {
            Admin? admin = this._admins_repository.Get(admin => admin.AdminId == id && admin.Enabled is true).FirstOrDefault();

            if (admin is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            admin.SetAccountProfile(profileDTO.UserLogin, profileDTO.UserPassword,
                profileDTO.UserPhone, profileDTO.UserName, profileDTO.UserSurname, profileDTO.UserPatronymic);

            await Task.Run(
                () => _admins_repository.Update(admin));

            await this._unitOfWork.Commit();
        }

        public async Task<AdminDTO> GetAccountById(long id)
        {
            Admin? admin = await Task.Run(
                () => this._admins_repository.Get(admin => admin.AdminId == id && admin.Enabled is true).FirstOrDefault());

            AdminDTO adminDTO = _mapper.Map<AdminDTO>(admin);

            return adminDTO;
        }

        public async Task<IList<AdminDTO>> GetAccountsList(int count)
        {
            IList<Admin> admins = await Task.Run(
                () => _admins_repository.Get(admin => admin.Enabled is true).Take(count).ToList());

            IList<AdminDTO> adminDTOs = _mapper.Map<IList<AdminDTO>>(admins);

            return adminDTOs;
        }

        public async Task<AdminDTO> GetAuthTokenByData(string login, string password)
        {
            IList<Admin> admins = await Task.Run(
                () => _admins_repository.Get(admin => admin.AccountLogin == login && admin.AccountPassword == password && admin.Enabled is true).ToList());

            if (admins.Count > 1)
            {
                throw new AccountAlreadyExistsException("Найдено несколько аккаунтов с заданным логином и паролём");
            }

            if (admins.Any() is false)
            {
                throw new AccountNotFoundException($"Аккаунт с указанным логином и паролём не найден");
            }

            AdminDTO adminDTO = this._mapper.Map<AdminDTO>(admins.First());

            return adminDTO;
        }
    }
}
