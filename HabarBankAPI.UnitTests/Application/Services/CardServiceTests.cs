using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Database;
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
    public class CardServiceTests
    {
        private CardService? _service { get; set; }

        public CardServiceTests()
        {
            UnitService();
        }

        internal void UnitService()
        {
            Mapper cardMapperA = AbstractMapper<Card, CardDTO>.MapperA;

            Mapper cardMapperB = AbstractMapper<CardDTO, Card>.MapperA;

            Mapper userMapper = AbstractMapper<User, UserDTO>.MapperA;

            using ApplicationDbContext context = new();

            GenericRepository<Card> cardsRepository = new(context);

            GenericRepository<User> usersRepository = new(context);

            GenericRepository<Substance> substanceRepository = new(context);

            GenericRepository<CardVariant> cardVariantRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            this._service = new CardService(cardMapperA, cardMapperB, userMapper, cardsRepository, usersRepository, substanceRepository, cardVariantRepository, unitOfWork);
        }

        [Fact]

        public async void GetAllCardsTest()
        {
            // Arrange

            UnitService();

            if (this._service is null)
            {
                return;
            }

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.GetCardsByUserId(1));

            // Assert

            Assert.True(exception is null);
        }

        [Fact]

        public async void CreateNewCardTest()
        {
            // Arrange

            UnitService();

            if (this._service is null)
            {
                return;
            }

            CardDTO cardDTO = new()
            {
                CardId = -1,
                CardNumber = string.Empty,
                CardVariantId = 1,
                RublesCount = 0,
                ImagePath = "Не указано",
                UserId = 1,
                Enabled = false
            };

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.CreateCard(1, cardDTO));

            // Assert

            Assert.True(exception is null);
        }

        [Fact]

        public async void AddPercentageTest()
        {
            // Arrange

            UnitService();

            if (this._service is null)
            {
                return;
            }

            CardDTO cardDTO = new()
            {
                CardId = -1,
                CardNumber = string.Empty,
                CardVariantId = 1,
                RublesCount = 0,
                ImagePath = "Не указано",
                UserId = 1,
                Enabled = true
            };

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.AddPercentage(1));

            // Assert

            Assert.True(exception is null);
        }

        [Fact]

        public async void EditCardEnabledTest()
        {
            // Arrange

            UnitService();

            if (this._service is null)
            {
                return;
            }

            CardDTO cardDTO = new()
            {
                CardId = -1,
                CardNumber = string.Empty,
                CardVariantId = 1,
                RublesCount = 0,
                ImagePath = "Не указано",
                UserId = 1,
                Enabled = true
            };

            await this._service.CreateCard(1, cardDTO);

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.EditCardEnabled(1, false));

            // Assert

            Assert.True(exception is null);
        }
    }
}
