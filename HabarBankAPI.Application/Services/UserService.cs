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
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
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

        private readonly GenericRepository<User> _users_repository;
        private readonly GenericRepository<UserLevel> _levels_repository;
        private readonly UnitOfWork _unitOfWork;

        public UserService(
            Mapper mapper, 
            GenericRepository<User> usersRepository,
            GenericRepository<UserLevel> levelsRepository,
            UnitOfWork unitOfWork) 
        { 
            this._mapper = mapper;
            this._users_repository = usersRepository;
            this._levels_repository = levelsRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateUserAccount(UserDTO userDTO)
        {
            UserLevel? accountLevel = this._levels_repository.GetWithInclude(x => x.Users)
                .FirstOrDefault(accountLevel => accountLevel.AccountLevelId == userDTO.UserLevelId && accountLevel.Enabled);

            if (accountLevel is null)
            {
                throw new AccountLevelNotFoundException($"Уровень с идентификатором {userDTO.UserLevelId} не найден");
            }

            UserFactory userFactory = new();

            User user = userFactory
                .WithLogin(userDTO.AccountLogin)
                .WithPassword(userDTO.AccountPassword)
                .WithPhone(userDTO.AccountPhone)
                .WithName(userDTO.AccountName)
                .WithSurname(userDTO.AccountSurname)
                .WithPatronymic(userDTO.AccountPatronymic)
                .WithAccountLevel(accountLevel)
            .Build();

            IList<User> users = await Task.Run(() => this._users_repository.Get(
                    user => user.AccountLogin == userDTO.AccountLogin && user.AccountPassword == userDTO.AccountPassword && user.Enabled is true).ToList());

            if (users.Any())
            {
                throw new AccountAlreadyExistsException("Аккаунт с заданным логином и паролём уже существует");
            }

            accountLevel.Users.Add(user);

            await Task.Run(
                () =>_levels_repository.Update(accountLevel));

            await this._unitOfWork.Commit();
        }

        public async Task EditAccountEnabled(long id, bool enabled)
        {
            User? user = this._users_repository.Get(user => user.UserId == id).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetEnabled(enabled);

            await Task.Run(
                () => this._users_repository.Update(user));

            await this._unitOfWork.Commit();
        }

        public async Task EditAccountProfile(long id, ProfileDTO profileDTO)
        {
            User? user = this._users_repository.Get(user => user.UserId == id && user.Enabled is true).FirstOrDefault();

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {id} не найден");
            }

            user.SetAccountProfile(profileDTO.UserLogin, profileDTO.UserPassword,
                profileDTO.UserPhone, profileDTO.UserName, profileDTO.UserSurname, profileDTO.UserPatronymic);

            await Task.Run(
                () => _users_repository.Update(user));

            await this._unitOfWork.Commit();
        }

        public async Task EditAccountStatus(long id, UserLevel level)
        {
            User? user = await Task.Run(
                () => this._users_repository.Get(user => user.UserId == id && user.Enabled is true).FirstOrDefault());

            if (user is null)
            {
                throw new AccountArgumentTypeException("Пользователь с идентификатором {id} не найден");
            }

            user.SetUserStatus(level);

            await Task.Run(
                () => _users_repository.Update(user));

            await this._unitOfWork.Commit();
        }

        public async Task EditAccountStatus(long id, long levelId)
        {
            User? user = await Task.Run(
                () => this._users_repository.Get(user => user.UserId == id && user.Enabled is true).FirstOrDefault());

            if (user is null)
            {
                throw new AccountArgumentTypeException("Пользователь с идентификатором {id} не найден");
            }

            UserLevel? userLevel = await Task.Run(
                () => this._levels_repository.Get(userLevel => userLevel.AccountLevelId == levelId && userLevel.Enabled is true).FirstOrDefault());

            if (userLevel is null)
            {
                throw new AccountLevelNotFoundException($"Уровень с идентификатором {levelId} не найден");
            }

            user.SetUserStatus(userLevel);

            await Task.Run(
                () => _users_repository.Update(user));

            await this._unitOfWork.Commit();
        }

        public async Task<UserDTO> GetAccountById(long id)
        {
            User? user = await Task.Run(
                () => this._users_repository.GetWithInclude(user => user.UserLevel)
                .FirstOrDefault(user => user.UserId == id && user.Enabled is true));

            UserDTO userDTO = PrepareUserDTO(user);

            return userDTO;
        }

        public async Task<IList<UserDTO>> GetAccountsList(int count)
        {
            IList<User> users = await Task.Run(
                () => _users_repository.GetWithInclude(user => user.UserLevel)
                .Where(user => user.Enabled is true).Take(count).ToList());

            IList<UserDTO> userDTOs = PreapreUserDTOs(users);

            return userDTOs;
        }

        public async Task<UserDTO> GetAuthTokenByData(string login, string password)
        {
            IList<User> users = await Task.Run(
                () => _users_repository.GetWithInclude(user => user.UserLevel)
                .Where(user => user.AccountLogin == login && user.AccountPassword == password && user.Enabled is true).ToList());

            if (users.Count > 1)
            {
                throw new AccountAlreadyExistsException("Найдено несколько аккаунтов с заданным логином и паролём");
            }

            if (users.Any() is false)
            {
                throw new AccountNotFoundException($"Аккаунт с указанным логином и паролём не найден");
            }

            UserDTO accountDTO = PrepareUserDTO(users.First());

            return accountDTO;
        }

        public Task<UserDTO> GetAuthTokenBySMS(string phone, string sms)
        {
            throw new NotImplementedException();
        }

        internal IList<UserDTO> PreapreUserDTOs(IList<User> users)
        {
            IList<UserDTO> userDTOs = new List<UserDTO>();

            foreach (User user in users)
            {
                UserDTO userDTO = this._mapper.Map<UserDTO>(user);

                userDTO.UserLevelId = user.UserLevel.AccountLevelId;

                userDTOs.Add(userDTO);
            }

            return userDTOs;
        }

        internal UserDTO PrepareUserDTO(User? user)
        {
            UserDTO userDTO = this._mapper.Map<UserDTO>(user);

            if (user is null)
            {
                return userDTO;
            }

            userDTO.UserLevelId = user.UserLevel.AccountLevelId;

            return userDTO;
        }
    }
}
