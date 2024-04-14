using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
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
using Action = System.Action;

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class CardVariantServiceTests
    {
        private CardVariantService? _service {  get; set; }

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<CardVariant> cardVariantRepository = new(context);

            GenericRepository<CardType> cardtypesRepository = new(context);

            GenericRepository<UserLevel> userLevelsRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<CardVariant, CardVariantDTO>.MapperA;

            Mapper mapperB = AbstractMapper<CardVariant, CardVariantDTO>.MapperB;
        }

        [Fact]

        public void GetAllCardVariantsTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Action action = async() => await this._service.GetAllCardVariants();

            Assert.ThrowsAny<Exception>(() => action);
        }

        [Fact]

        public async void GetCardVariantByIdTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(async () =>
            {
                CardVariantDTO cardVariantDTO = await this._service.GetCardVariantById(1);
            });

            Assert.True(exception is null);
        }
    }
}
