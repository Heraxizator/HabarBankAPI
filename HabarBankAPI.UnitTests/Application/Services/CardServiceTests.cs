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

        internal void UnitService()
        {
            Mapper cardMapperA = AbstractMapper<Card, CardDTO>.MapperA;

            Mapper cardMapperB = AbstractMapper<CardDTO, Card>.MapperA;

            Mapper userMapper = AbstractMapper<User, UserDTO>.MapperA;

            ApplicationDbContext context = new();

            GenericRepository<Card> cardsRepository = new(context);

            GenericRepository<User> usersRepository = new(context);

            GenericRepository<Substance> substanceRepository = new(context);

            GenericRepository<CardVariant> cardVariantRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            this._service = new CardService(cardMapperA, cardMapperB, userMapper, cardsRepository, usersRepository, substanceRepository, cardVariantRepository, unitOfWork);
        }

    }
}
