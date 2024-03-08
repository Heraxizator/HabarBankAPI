using AutoMapper;
using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.AccountLevel;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class AccountLevelService : IAccountLevelService
    {
        private readonly IGenericRepository<AccountLevel> _repository;
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public AccountLevelService(IGenericRepository<AccountLevel> repository, Mapper mapperA, Mapper mapperB) 
        {
            this._repository = repository;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewAccountLevel(AccountLevelDTO accountLevelDTO)
        {
            AccountLevel accountLevel = this._mapperA.Map<AccountLevel>(accountLevelDTO);

            await Task.Run(() => this._repository.Create(accountLevel));
        }

        public async Task<AccountLevelDTO> GetAccountLevelById(long id)
        {
            AccountLevelByIdSpecification specification = new();

            AccountLevel? accountLevel = await Task.Run(
                () => this._repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault());

            AccountLevelDTO accountLevelDTO = this._mapperB.Map<AccountLevelDTO>(accountLevel);

            return accountLevelDTO;
        }

        public async Task<IList<AccountLevelDTO>> GetAllAccountLevels()
        {
            AcountLevelsListSpecification specification = new();

            IList<AccountLevel> accountLevels = await Task.Run(
                () => this._repository.Get(x => specification.IsSatisfiedBy(x)).ToList());

            IList<AccountLevelDTO> accountLevelDTOs = this._mapperB.Map<IList<AccountLevelDTO>>(accountLevels);

            return accountLevelDTOs;
        }

        public async Task SetAccountLevelEnabled(long levelId, bool levelEnabled)
        {
            AccountLevelByIdSpecification specification = new();

            AccountLevel? accountLevel = await Task.Run(
                () => this._repository.Get(x => specification.IsSatisfiedBy((x, levelId))).FirstOrDefault());

            if (accountLevel is null)
            {
                throw new AccountLevelNotFoundException($"Не удалось найти уровень аккаунта с идентификатором {levelId}");
            }

            accountLevel.SetEnabled(levelEnabled);

            await Task.Run(() => this._repository.Update(accountLevel));
        }
    }
}
