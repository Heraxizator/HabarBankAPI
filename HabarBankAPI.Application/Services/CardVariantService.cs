using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class CardVariantService : ICardVariantService
    {
        private readonly GenericRepository<CardVariant> _cardvariants_repository;
        private readonly GenericRepository<CardType> _cardtypes_repository;
        private readonly GenericRepository<UserLevel> _userlevels_repository;

        private readonly UnitOfWork _unitOfWork;

        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public CardVariantService(
            GenericRepository<CardVariant> cardVariantRepository,
            GenericRepository<CardType> cardTypeRepository,
            GenericRepository<UserLevel> userLevelRepository,
            UnitOfWork unitOfWork,
            Mapper mapperA,
            Mapper mapperB)
        {
            this._cardvariants_repository = cardVariantRepository;
            this._cardtypes_repository = cardTypeRepository;
            this._userlevels_repository = userLevelRepository;
            this._unitOfWork = unitOfWork;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewCardVariant(CardVariantDTO cardVariantDTO)
        {
            CardType? cardType = this._cardtypes_repository.GetWithInclude(cardType => cardType.CardVariants).FirstOrDefault(
                cardType => cardType.CardTypeId == cardVariantDTO.CardTypeId && cardType.Enabled is true);

            if (cardType is null)
            {
                throw new CardTypeNotFoundException($"Тип карты с идентификатором {cardVariantDTO.CardTypeId} не найден");
            }

            UserLevel? userLevel = this._userlevels_repository.Get(
                userLevel => userLevel.AccountLevelId == cardVariantDTO.UserLevelId && userLevel.Enabled is true).FirstOrDefault();

            if (userLevel is null)
            {
                throw new AccountLevelNotFoundException($"Уровень пользователя с идентификатором {cardVariantDTO.UserLevelId} не найден");
            }

            CardVariantFactory cardVariantFactory = new();

            CardVariant cardVariant = cardVariantFactory
                .WithCardType(cardType)
                .WithUserLevel(userLevel)
                .WithPercentage(cardVariantDTO.Percentage)
                .WithText(cardVariantDTO.Text)
                .WithName(cardVariantDTO.Name)
                .Build();

            cardType.CardVariants.Add(cardVariant);

            await Task.Run(() => this._cardtypes_repository.Update(cardType));

            await this._unitOfWork.Commit();
        }

        public async Task<IList<CardVariantDTO>> GetAllCardVariants()
        {
            IList<CardVariant> cardVariants = await Task.Run(
                () => this._cardvariants_repository.GetWithInclude(x => x.CardType, x => x.AccountLevel).Where(x => x.Enabled is true).ToList());

            IList<CardVariantDTO> cardVariantDTOs = PrepareCardVariantDTOS(cardVariants);

            return cardVariantDTOs;
        }

        public async Task<CardVariantDTO> GetCardVariantById(long id)
        {
            CardVariant? cardVariant = await Task.Run(() => this._cardvariants_repository
            .GetWithInclude(x => x.CardType, x => x.AccountLevel).FirstOrDefault(x => x.CardVariantId == id && x.Enabled is true));

            CardVariantDTO cardVariantDTO = PrepareCardVariantDTO(cardVariant);

            return cardVariantDTO;
        }

        public async Task SetCardVariantEnabled(long id, bool enabled)
        {
            CardVariant? cardVariant = await Task.Run(() => this._cardvariants_repository.FindById(id));

            if (cardVariant is null)
            {
                throw new CardVariantNotFoundException($"Не удалось найти вариант карты с идентификатором {id}");
            }

            cardVariant.SetEnabled(enabled);

            await Task.Run(() => this._cardvariants_repository.Update(cardVariant));

            await this._unitOfWork.Commit();
        }

        internal CardVariantDTO PrepareCardVariantDTO(CardVariant? cardVariant)
        {
            CardVariantDTO cardVariantDTO = this._mapperB.Map<CardVariantDTO>(cardVariant);

            cardVariantDTO.CardTypeId = cardVariant?.CardType is null ? 0 : cardVariant.CardType.CardTypeId;

            cardVariantDTO.UserLevelId = cardVariant?.AccountLevel is null ? 0 : cardVariant.AccountLevel.AccountLevelId;

            return cardVariantDTO;
        }

        internal IList<CardVariantDTO> PrepareCardVariantDTOS(IList<CardVariant> cardVariants)
        {
            IList<CardVariantDTO> cardVariantDTOs = new List<CardVariantDTO>();

            foreach (CardVariant cardVariant in cardVariants)
            {
                CardVariantDTO cardVariantDTO = this._mapperB.Map<CardVariantDTO>(cardVariant);

                cardVariantDTO.CardTypeId = cardVariant.CardType is null ? 0 : cardVariant.CardType.CardTypeId;

                cardVariantDTO.UserLevelId = cardVariant.AccountLevel is null ? 0 : cardVariant.AccountLevel.AccountLevelId;

                cardVariantDTOs.Add(cardVariantDTO);
            }

            return cardVariantDTOs;
        }
    }
}
