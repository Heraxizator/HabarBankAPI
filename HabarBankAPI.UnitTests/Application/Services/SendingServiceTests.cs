using AutoMapper;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Application;
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
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Entities.Transfer;

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class SendingServiceTests
    {
        private SendingService? _service { get; set; }

        public SendingServiceTests()
        {
            UnitService();
        }

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<Sending> sendingRepositoty = new(context);

            GenericRepository<Card> cardsRepository = new(context);

            GenericRepository<User> usersRepository = new(context);

            GenericRepository<OperationType> operationTypesRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<SendingDTO, Sending>.MapperA;
            Mapper mapperB = AbstractMapper<Sending, SendingDTO>.MapperA;

            this._service = new SendingService(mapperA, mapperB, sendingRepositoty, cardsRepository, usersRepository, operationTypesRepository, unitOfWork);
        }

        [Fact]

        public async void CreateNewTransferTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            SendingDTO sendingDTO = new()
            {
                SendingId = -1,
                SubstanceId = 1,
                SubstanceRecipientId = 2,
                SubstanceSenderId = 1,
                OperationDateTime = DateTime.Now,
                OperationTypeId = 1,
                RublesCount = 100,
                Enabled = false
            };

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.CreateTransfer(sendingDTO));

            // Assert

            Assert.True(exception is null);
        }
    }
}
