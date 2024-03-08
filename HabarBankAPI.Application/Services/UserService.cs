using AutoMapper;
using HabarBankAPI.Application.DTO;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Factories;
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
        private readonly IGenericRepository<User> _users_repository;
        private readonly IGenericRepository<AccountLevel> _levels_repository;

        public UserService(Mapper mapper, 
            IGenericRepository<User> usersRepository,
            IGenericRepository<AccountLevel> levelsRepository) 
        { 
            this._mapper = mapper;
            this._users_repository = usersRepository;
            this._levels_repository = levelsRepository;
        }

        public async Task CreateUserAccount(UserDTO userDTO)
        {
            UserFactory userFactory = new();

            userFactory.WithLogin(userDTO.AccountLogin);

            userFactory.WithPassword(userDTO.AccountPassword);

            userFactory.WithPhone(userDTO.AccountPhone);

            userFactory.WithName(userDTO.AccountName);

            userFactory.WithSurname(userDTO.AccountSurname);

            userFactory.WithPatronymic(userDTO.AccountPatronymic);

            userFactory.WithAccountLevelId(userDTO.AccountLevelId);

            User user = userFactory.Build();

            AccountLevelByIdSpecification accountLevelByIdSpecification = new();

            AccountLevel? accountLevel = await Task.Run(() => this._levels_repository.Get(
                x => accountLevelByIdSpecification.IsSatisfiedBy((x, user.AccountLevelId))).FirstOrDefault());

            if (accountLevel is null)
            {
                throw new AccountLevelNotFoundException($"Не найден уровень аккаунта с идентификатором {user.AccountLevelId}");
            }

            AuthByDataSpecification authByDataSpecification = new();

            IList<User> users = await Task.Run(() => this._users_repository.Get(
                    x => authByDataSpecification.IsSatisfiedBy((x, user.AccountLogin, user.AccountPassword))).ToList());

            if (users.Any())
            {
                throw new AccountAlreadyExistsException("Аккаунт с заданным логином и паролём уже существует");
            }

            await Task.Run(
                () =>_users_repository.Create(user));
        }

        public async Task EditAccountEnabled(long id, bool enabled)
        {
            AccountByIdSpecification specification = new();

            User? user = this._users_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetEnabled(enabled);

            await Task.Run(
                () => this._users_repository.Update(user));
        }

        public async Task EditAccountProfile(long id, ProfileDTO profileDTO)
        {
            AccountByIdSpecification specification = new();

            User? user = this._users_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetAccountProfile(profileDTO.UserLogin, profileDTO.UserPassword,
                profileDTO.UserPhone, profileDTO.UserName, profileDTO.UserSurname, profileDTO.UserPatronymic);

            await Task.Run(
                () => _users_repository.Update(user));
        }

        public async Task EditAccountStatus(long id, AccountLevel level)
        {
            AccountByIdSpecification specification = new();

            User? user = await Task.Run(
                () => this._users_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault());

            if (user is null)
            {
                throw new AccountArgumentTypeException("Пользователь с идентификатором {id} не найден");
            }

            user.SetUserStatus(level.AccountLevelId);

            await Task.Run(
                () => _users_repository.Update(user));
        }

        public async Task<UserDTO> GetAccountById(long id)
        {
            AccountByIdSpecification specification = new();

            User? user = await Task.Run(
                () => this._users_repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault());

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<IList<UserDTO>> GetAccountsList(int count)
        {
            AccountsListSpecification specification = new();

            IList<User> users = await Task.Run(
                () => _users_repository.Get(x => specification.IsSatisfiedBy(x)).Take(count).ToList());

            IList<UserDTO> userDTOs = _mapper.Map<IList<UserDTO>>(users);

            return userDTOs;
        }

        public async Task<UserDTO> GetAuthTokenByData(string login, string password)
        {
            AuthByDataSpecification specification = new();

            IList<User> users = await Task.Run(
                () => _users_repository.Get(x => specification.IsSatisfiedBy((x, login, password))).ToList());

            if (users.Count > 1)
            {
                throw new AccountAlreadyExistsException("Найдено несколько аккаунтов с заданным логином и паролём");
            }

            if (users.Any() is false)
            {
                throw new AccountNotFoundException($"Аккаунт с указанным логином и паролём не найден");
            }

            UserDTO accountDTO = this._mapper.Map<UserDTO>(users.First());

            return accountDTO;
        }

        public Task<UserDTO> GetAuthTokenBySMS(string phone, string sms)
        {
            throw new NotImplementedException();
        }
    }
}
