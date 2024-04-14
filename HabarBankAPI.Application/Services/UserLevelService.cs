using AutoMapper;
using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class UserLevelService : IUserLevelService
    {
        private readonly IGenericRepository<UserLevel> _repository;
        private readonly AppUnitOfWork _unitOfWork;

        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public UserLevelService(
            IGenericRepository<UserLevel> repository,
            AppUnitOfWork unitOfWork,
            Mapper mapperA,
            Mapper mapperB) 
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewAccountLevel(AccountLevelDTO accountLevelDTO)
        {
            UserLevelFactory userLevelFactory = new();

            UserLevel userLevel = userLevelFactory
                .WithName(accountLevelDTO.Name)
            .Build();

            await Task.Run(() => this._repository.Create(userLevel));

            await this._unitOfWork.Commit();
        }

        public async Task<AccountLevelDTO> GetAccountLevelById(long id)
        {
            AccountLevelByIdSpecification specification = new();

            UserLevel? userLevel = await Task.Run(
                () => this._repository.Get(userLevel => specification.IsSatisfiedBy((userLevel, id))).FirstOrDefault());

            AccountLevelDTO accountLevelDTO = this._mapperB.Map<AccountLevelDTO>(userLevel);

            return accountLevelDTO;
        }

        public async Task<IList<AccountLevelDTO>> GetAllAccountLevels()
        {
            AcountLevelsListSpecification specification = new();

            IList<UserLevel> accountLevels = await Task.Run(
                () => this._repository.Get(userLevel=> specification.IsSatisfiedBy(userLevel)).ToList());

            IList<AccountLevelDTO> accountLevelDTOs = this._mapperB.Map<IList<AccountLevelDTO>>(accountLevels);

            return accountLevelDTOs;
        }

        public async Task SetAccountLevelEnabled(long accountLevelId, bool levelEnabled)
        {
            AccountLevelByIdSpecification specification = new();

            UserLevel? userLevel= await Task.Run(
                () => this._repository.Get(userLevel=> specification.IsSatisfiedBy((userLevel, accountLevelId))).FirstOrDefault());
            
            if (userLevel is null)
            {
                throw new AccountLevelNotFoundException($"Не удалось найти уровень аккаунта с идентификатором {accountLevelId}");
            }

            userLevel.SetEnabled(levelEnabled);

            await Task.Run(() => this._repository.Update(userLevel));

            await this._unitOfWork.Commit();
        }
    }
}
