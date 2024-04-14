using AutoMapper;
using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Application.DTO.Admins;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = System.Action;

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class UserLevelServiceTests
    {
        private UserLevelService? _service { get; set; }

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<UserLevel> userlevelRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<UserLevel, AccountLevelDTO>.MapperA;

            Mapper mapperB = AbstractMapper<UserLevel, AccountLevelDTO>.MapperB;

            this._service = new UserLevelService(userlevelRepository, unitOfWork, mapperA, mapperB);
        }

        [Fact]

        public async void GetAllUserLevelsTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.GetAllAccountLevels());

            Assert.True(exception is null);
        }
    }
}
