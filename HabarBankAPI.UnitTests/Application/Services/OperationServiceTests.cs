using AutoMapper;
using HabarBankAPI.Application;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class OperationServiceTests
    {
        private OperationService? _service { get; set; }

        public OperationServiceTests()
        {
            UnitService();
        }

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<Operation> operationRepository = new(context);

            GenericRepository<OperationType> operationtypesRepository = new(context);

            GenericRepository<Card> cardRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<Operation, OperationDTO>.MapperA;

            Mapper mapperB = AbstractMapper<Operation, OperationDTO>.MapperB;

            this._service = new OperationService(operationRepository, operationtypesRepository, cardRepository, unitOfWork, mapperA, mapperB);
        }

        [Fact]

        public async void GetAllOperationsTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.GetAllActions(1000));

            // Assert

            Assert.True(exception is null);
        }

        [Fact]

        public async void GetAllOperationsByEntityIdTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.GetActionsByEntityId(1));

            // Assert

            Assert.True(exception is null);
        }

        [Fact]

        public async void CreateNewOperation()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            OperationDTO operationDTO = new()
            {
                OperationId = -1,
                SubstanceId = 1,
                OperationDateTime = DateTime.Now,
                OperationTypeId = 1,
                Enabled = false
            };

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.CreateNewAction(operationDTO));

            Assert.True(exception is null);
        }
    }
}
