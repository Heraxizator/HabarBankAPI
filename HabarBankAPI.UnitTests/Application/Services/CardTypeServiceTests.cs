using AutoMapper;
using HabarBankAPI.Application.DTO.Admins;
using HabarBankAPI.Application.DTO.Cards;
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

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class CardTypeServiceTests
    {
        private CardTypeService? _service { get; set; }

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<CardType> cardTypesRepository = new(context);

            UnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<CardType, CardTypeDTO>.MapperA;

            Mapper mapperB = AbstractMapper<CardType, CardTypeDTO>.MapperB;
        }

        [Fact]

        public async void GetAllCardTypesTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            IList<CardTypeDTO> cardTypeDTOs = await this._service.GetAllCardTypes();

            Assert.True(cardTypeDTOs.Any());
        }

        [Fact]

        public async void GetCardTypeById()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(async () =>
            {
                CardTypeDTO cardTypeDTO = await this._service.GetCardTypeById(1);
            });

            Assert.True(exception is null);
        }
    }
}
