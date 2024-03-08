using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Constants;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Card;
using HabarBankAPI.Domain.Entities.Substance;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = HabarBankAPI.Domain.Entities.Operation.Action;

namespace HabarBankAPI.Application.Services
{
    public class CardService : ICardService
    {
        private readonly Mapper _card_mapperA;
        private readonly Mapper _card_mapperB;
        private readonly Mapper _user_mapper;

        private readonly IGenericRepository<Card> _cards_repository;
        private readonly IGenericRepository<Account> _users_repository;
        private readonly IGenericRepository<Substance> _entities_repository;

        public CardService(
            Mapper card_mapperA,
            Mapper card_mapperB,
            Mapper user_mapper,

            IGenericRepository<Card> cards_repository,
            IGenericRepository<Account> users_repository,
            IGenericRepository<Substance> entities_repository)
        {
            this._card_mapperA = card_mapperA;
            this._card_mapperB = card_mapperB;
            this._user_mapper = user_mapper;

            this._cards_repository = cards_repository;
            this._users_repository = users_repository;
            this._entities_repository = entities_repository;
        }
        public async Task AddPercentage(long id)
        {
            Card? card = await Task.Run(
                () => this._cards_repository.Get(x => x.SubstanceId == id && x.Enabled is true).FirstOrDefault());

            if (card is null)
            {
                throw new CardNotFoundException($"Карта с идентификатором {id} не найдена");
            }

            card.SetPercentages(10);

            await Task.Run(() => this._cards_repository.Update(card));
        }

        public async Task CreateCard(long userId, CardDTO cardDTO)
        {
            Account? user = await Task.Run(
                () => this._users_repository.Get(x => x.AccountId == userId && x.Enabled is true).FirstOrDefault());

            if (user is null)
            {
                throw new AccountNotFoundException($"Аккаунт с идентификатором {userId} не найден");
            }

            CardFactory cardFactory = new();

            cardFactory.WithImagePath(cardDTO.ImagePath);

            cardFactory.WithCardVariantId(cardDTO.CardVariantId);

            cardFactory.WithRublesCount(cardDTO.RublesCount);

            Card card = cardFactory.Build();

            await Task.Run(() => this._cards_repository.Create(card));
        }

        public async Task EditCardEnabled(long id, bool enabled)
        {
            Card? card = await Task.Run(
                () => this._cards_repository.Get(x => x.SubstanceId == id && x.Enabled is true).FirstOrDefault());

            if (card is null)
            {
                throw new CardNotFoundException($"Карта с идентификатором {id} не найдена");
            }

            card.SetEnabled(enabled);

            await Task.Run(() => this._cards_repository.Update(card));
        }

        public async Task<CardDTO> GetCardData(long id)
        {
            Card? card = await Task.Run(
                () => this._cards_repository.Get(x => x.SubstanceId == id && x.Enabled is true).FirstOrDefault());

            CardDTO cardDTO = this._card_mapperB.Map<CardDTO>(card);

            return cardDTO;
        }

        public async Task<IList<CardDTO>> GetCardsByUserId(long userId)
        {
            IList<Substance> entities = await Task.Run(
                () => this._entities_repository.Get(x => x.AccountId == userId && x.Enabled is true).ToList());

            IList<long> entitiesIds = entities.Select(x => x.SubstanceId).ToList();

            IList<Card> cards = await Task.Run(
                () => this._cards_repository.Get(x => entitiesIds.Contains(x.SubstanceId)).ToList());

            IList<CardDTO> cardDTOs = this._card_mapperB.Map<IList<CardDTO>>(cards);

            return cardDTOs;
        }
    }
}
