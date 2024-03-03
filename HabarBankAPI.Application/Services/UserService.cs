using AutoMapper;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Specifications.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly Mapper _mapper;
        private readonly IGenericRepository<Account> _accounts_repository;
        private readonly IGenericRepository<AccountLevel> _levels_repository;

        public UserService(Mapper mapper, 
            IGenericRepository<Account> accountsRepository,
            IGenericRepository<AccountLevel> levelsRepository) 
        { 
            this._mapper = mapper;
            this._accounts_repository = accountsRepository;
            this._levels_repository = levelsRepository;
        }
        
        public async Task CreateAccount(UserDTO userDTO)
        {
            PhoneSpecification phoneSpecification = new();

            if (!phoneSpecification.IsSatisfiedBy(userDTO.AccountPhone))
            {
                throw new BadPhoneException("Номер телефона должен включать в себя 12 символов: знак + и 11 цифр");
            }

            LoginSpecification loginSpecification = new();

            if (!loginSpecification.IsSatisfiedBy(userDTO.AccountLogin))
            {
                throw new BadLoginException("Логин содержит не менее 5 символов и хотя бы одну букву");
            }

            PasswordSpecification passwordSpecification = new();

            if (!passwordSpecification.IsSatisfiedBy(userDTO.AccountPassword))
            {
                throw new BadPasswordException("Пароль содержит не менее 6 символов");
            }

            NameSpecification nameSpecification = new();

            if (!nameSpecification.IsSatisfiedBy(userDTO.AccountName))
            {
                throw new BadNameException("Имя содержит только буквы, где первая является заглавной");
            }

            SurnameSpecification surnameSpecification = new();

            if (!surnameSpecification.IsSatisfiedBy(userDTO.AccountSurname))
            {
                throw new BadSurnameException("Фамилия содержит только буквы, где первая является заглавной");
            }

            PatronymicSpecification patronymicSpecification = new();

            if (!patronymicSpecification.IsSatisfiedBy(userDTO.AccountPatronymic))
            {
                throw new BadPatronymicException("Отчество содержит не менее пяти букв, где первая является заглавной");
            }

            AccountLevel? level = this._levels_repository.Get(x => x.Enabled is true).FirstOrDefault();

            if (level is null)
            {
                throw new AccountLevelNotFoundException($"Уровень аккаунта не найден");
            }

            Account account = new();

            account.SetAccountProfile(
                userDTO.AccountLogin, userDTO.AccountPassword, userDTO.AccountPhone, userDTO.AccountName, userDTO.AccountSurname, userDTO.AccountPatronymic);

            account.SetEnabled(true);

            account.SetAccountStatus(level.AccountLevelId);

            await Task.Run(
                () =>_accounts_repository.Create(account));
        }

        public async Task EditAccountEnabled(int id, bool enabled)
        {
            Account? user = this._accounts_repository.Get(x => x.AccountId == id).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetEnabled(enabled);

            await Task.Run(
                () => this._accounts_repository.Update(user));
        }

        public async Task EditAccountProfile(int id, ProfileDTO profileDTO)
        {
            Account? user = this._accounts_repository.Get(x => x.AccountId == id).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetAccountProfile(profileDTO.UserLogin, profileDTO.UserPassword,
                profileDTO.UserPhone, profileDTO.UserName, profileDTO.UserSurname, profileDTO.UserPatronymic);

            await Task.Run(
                () => _accounts_repository.Update(user));
        }

        public async Task EditAccountStatus(int id, AccountLevel level)
        {
            Account? user = this._accounts_repository.Get(x => x.AccountId == id).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetAccountStatus(level.AccountLevelId);

            await Task.Run(
                () => _accounts_repository.Update(user));
        }

        public async Task<UserDTO> GetAccountById(int id)
        {
            Account? user = await Task.Run(
                () => this._accounts_repository.Get(x => x.AccountId == id).FirstOrDefault());

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<IList<UserDTO>> GetAccountsList(int count)
        {
            IList<Account> users = await Task.Run(
                () => _accounts_repository.Get(x => true).Take(count).ToList());

            IList<UserDTO> userDTOs = _mapper.Map<IList<UserDTO>>(users);

            return userDTOs;
        }

        public async Task<UserDTO> GetAuthTokenByData(string login, string password)
        {
            Account? user = await Task.Run(
                () => _accounts_repository.Get(x => x.AccountLogin == login && x.AccountPassword == password && x.Enabled == true).FirstOrDefault());

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с указанным логином и паролём не найден");
            }

            UserDTO userDTO = this._mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public Task<UserDTO> GetAuthTokenBySMS(string phone, string sms)
        {
            throw new NotImplementedException();
        }
    }
}
