using AutoMapper;
using HabarBankAPI.Application.DTO;
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

namespace HabarBankAPI.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly Mapper _mapper;
        private readonly IGenericRepository<Admin> _admins_repository;

        public AdminService(Mapper mapper,
            IGenericRepository<Admin> adminsRepository)
        {
            this._mapper = mapper;
            this._admins_repository = adminsRepository;
        }

        public async Task CreateAdminAccount(AdminDTO adminDTO)
        {
            AdminFactory adminFactory = new();

            adminFactory.WithLogin(adminDTO.AccountLogin);

            adminFactory.WithPassword(adminDTO.AccountPassword);

            adminFactory.WithPhone(adminDTO.AccountPhone);

            adminFactory.WithName(adminDTO.AccountName);

            adminFactory.WithSurname(adminDTO.AccountSurname);

            adminFactory.WithPatronymic(adminDTO.AccountPatronymic);

            Admin admin = adminFactory.Build();

            AuthByDataSpecification authByDataSpecification = new();

            IList<Admin> admins = await Task.Run(() => this._admins_repository.Get(
                    x => authByDataSpecification.IsSatisfiedBy((x, admin.AccountLogin, admin.AccountPassword))).ToList());

            if (admins.Any())
            {
                throw new AccountAlreadyExistsException("Аккаунт с заданным логином и паролём уже существует");
            }

            await Task.Run(
                () => _admins_repository.Create(admin));
        }

        public async Task EditAccountEnabled(long id, bool enabled)
        {
            AccountByIdSpecification specification = new();

            Admin? admin = this._admins_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault();

            if (admin is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            admin.SetEnabled(enabled);

            await Task.Run(
                () => this._admins_repository.Update(admin));
        }

        public async Task EditAccountProfile(long id, ProfileDTO profileDTO)
        {
            AccountByIdSpecification specification = new();

            Admin? admin = this._admins_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault();

            if (admin is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            admin.SetAccountProfile(profileDTO.UserLogin, profileDTO.UserPassword,
                profileDTO.UserPhone, profileDTO.UserName, profileDTO.UserSurname, profileDTO.UserPatronymic);

            await Task.Run(
                () => _admins_repository.Update(admin));
        }

        public async Task<AdminDTO> GetAccountById(long id)
        {
            AccountByIdSpecification specification = new();

            Admin? admin = await Task.Run(
                () => this._admins_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault());

            AdminDTO adminDTO = _mapper.Map<AdminDTO>(admin);

            return adminDTO;
        }

        public async Task<IList<AdminDTO>> GetAccountsList(int count)
        {
            AccountsListSpecification specification = new();

            IList<Admin> admins = await Task.Run(
                () => _admins_repository.Get(x => specification.IsSatisfiedBy(x)).Take(count).ToList());

            IList<AdminDTO> adminDTOs = _mapper.Map<IList<AdminDTO>>(admins);

            return adminDTOs;
        }

        public async Task<AdminDTO> GetAuthTokenByData(string login, string password)
        {
            AuthByDataSpecification specification = new();

            IList<Admin> admins = await Task.Run(
                () => _admins_repository.Get(x => specification.IsSatisfiedBy((x, login, password))).ToList());

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
