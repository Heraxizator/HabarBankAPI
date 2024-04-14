using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Database;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Constants;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Exceptions.User;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Share;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = HabarBankAPI.Domain.Entities.Operation;

namespace HabarBankAPI.Application.Services
{
    public class CardService : ICardService
    {
        private readonly Mapper _card_mapperA;
        private readonly Mapper _card_mapperB;
        private readonly Mapper _user_mapper;

        private readonly GenericRepository<Card> _cards_repository;
        private readonly GenericRepository<User> _users_repository;
        private readonly GenericRepository<Substance> _entities_repository;
        private readonly GenericRepository<CardVariant> _cardvariants_repository;
        private readonly IUnitOfWork _unitOfWork;

        public CardService(
            Mapper card_mapperA,
            Mapper card_mapperB,
            Mapper user_mapper,

            GenericRepository<Card> cards_repository,
            GenericRepository<User> users_repository,
            GenericRepository<Substance> entities_repository,
            GenericRepository<CardVariant> cardvariants_repository,
            AppUnitOfWork unitOfWork)
        {
            this._card_mapperA = card_mapperA;
            this._card_mapperB = card_mapperB;
            this._user_mapper = user_mapper;

            this._cards_repository = cards_repository;
            this._users_repository = users_repository;
            this._entities_repository = entities_repository;
            this._cardvariants_repository = cardvariants_repository;
            this._unitOfWork = unitOfWork;
        }
        public async Task AddPercentage(long id)
        {
            Card? card = await Task.Run(
                () => this._cards_repository.Get(card => card.CardId == id && card.Enabled is true).FirstOrDefault());

            if (card is null)
            {
                throw new CardNotFoundException($"Карта с идентификатором {id} не найдена");
            }

            card.SetPercentages();

            await Task.Run(() => this._cards_repository.Update(card));

            await this._unitOfWork.Commit();
        }

        public async Task CreateCard(long userId, CardDTO cardDTO)
        {
            User? user = await Task.Run(
                () => this._users_repository
                .GetWithInclude(user => user.Cards, user => user.UserLevel)
                .FirstOrDefault(user => user.UserId == userId && user.Enabled is true));

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {userId} не найден");
            }

            CardVariant? cardVariant = await Task.Run(
                () => this._cardvariants_repository.GetWithInclude(cardVariant => cardVariant.Cards).FirstOrDefault(
                    cardVariant => cardVariant.CardVariantId == cardDTO.CardVariantId && cardVariant.Enabled is true));

            if (cardVariant is null)
            {
                throw new CardVariantNotFoundException($"Вариант карты с идентификатором {cardDTO.CardVariantId} не найден");
            }

            CardFactory cardFactory = new();

            Card card = cardFactory
                .WithImagePath(cardDTO.ImagePath)
                .WithRublesCount(cardDTO.RublesCount)
                .WithCardUser(user)
                .WithCardVariant(cardVariant)
            .Build();

            user.Cards.Clear();

            user.Cards.Add(card);

            await Task.Run(() => this._users_repository.Update(user));

            await this._unitOfWork.Commit();
        }

        public async Task EditCardEnabled(long id, bool enabled)
        {
            Card? card = await Task.Run(
                () => this._cards_repository.Get(card => card.CardId == id && card.Enabled is true).FirstOrDefault());

            if (card is null)
            {
                throw new CardNotFoundException($"Карта с идентификатором {id} не найдена");
            }

            card.SetEnabled(enabled);

            await Task.Run(() => this._cards_repository.Update(card));

            await this._unitOfWork.Commit();
        }

        public async Task<CardDTO> GetCardData(long id)
        {
            Card? card = await Task.Run(
                () => this._cards_repository.GetWithInclude(
                    card => card.CardVariant, card => card.User).FirstOrDefault(card => card.CardId == id && card.Enabled is true));

            CardDTO cardDTO = PrepareCardDTO(card);

            return cardDTO;
        }

        public async Task<IList<CardDTO>> GetCardsByUserId(long userId)
        {
            IList<Card> cards = await Task.Run(
                () => this._cards_repository.GetWithInclude(card => card.CardVariant, card => card.User)
                .Where(card => card.User?.UserId == userId && card.Enabled is true).ToList());

            IList<CardDTO> cardDTOs = PreapreCardDTOs(cards);

            return cardDTOs;
        }

        internal IList<CardDTO> PreapreCardDTOs(IList<Card> cards)
        {
            IList<CardDTO> cardDTOs = new List<CardDTO>();

            foreach (Card card in cards)
            {
                CardDTO cardDTO = PrepareCardDTO(card);

                cardDTOs.Add(cardDTO);
            }

            return cardDTOs;
        }

        internal CardDTO PrepareCardDTO(Card? card)
        {
            CardDTO cardDTO = this._card_mapperB.Map<CardDTO>(card);

            cardDTO.CardVariantId = card?.CardVariant is null ? 0 : card.CardVariant.CardVariantId;

            cardDTO.UserId = card?.User is null ? 0 : card.User.UserId;

            return cardDTO;
        }
    }
}
