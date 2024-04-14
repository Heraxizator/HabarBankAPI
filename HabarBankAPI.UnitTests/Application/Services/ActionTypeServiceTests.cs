using AutoMapper;
using HabarBankAPI.Application;
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
    public class ActionTypeServiceTests
    {
        private ActionTypeService? _service { get; set; }

        private void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<OperationType> repository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<OperationType, OperationTypeDTO>.MapperA;

            Mapper mapperB = AbstractMapper<OperationType, OperationTypeDTO>.MapperB;

            this._service = new ActionTypeService(repository, unitOfWork, mapperA, mapperB);
        }


        [Fact]

        public async void GetAllActionTypesTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.GetAllActionTypes());

            Assert.True(exception is null);
        }

        [Fact]

        public async void GetActionTypeByIdTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(async () =>
            {
                OperationTypeDTO operationTypeDTO = await this._service.GetActionTypeById(1);
            });

            Assert.True(exception is null);

        }
    }
}
