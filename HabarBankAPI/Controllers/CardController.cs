using AutoMapper;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Domain.Entities;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/cards/")]
    [ApiController]
    public class CardController : ControllerBase
    {

        private readonly GenericRepository<Card> _cards_repository;
        private readonly GenericRepository<Account> _users_repository;
        private readonly GenericRepository<Substance> _entities_repository;

        private readonly CardService _service;
        private readonly Mapper _card_mapperA;
        private readonly Mapper _card_mapperB;
        private readonly Mapper _user_mapper;

        public CardController()
        {
            ApplicationDbContext context = new();

            this._card_mapperA = AbstractMapper<CardDTO, Card>.MapperA;

            this._card_mapperB = AbstractMapper<Card, CardDTO>.MapperA;

            this._user_mapper = AbstractMapper<UserDTO, Account>.MapperA;

            this._cards_repository = new GenericRepository<Card>(context);

            this._users_repository = new GenericRepository<Account>(context);

            this._entities_repository = new GenericRepository<Substance>(context);

            this._service = new CardService(
                _card_mapperA, _card_mapperB, _user_mapper, _cards_repository, _users_repository, _entities_repository);
        }

        [HttpGet]
        public async Task<ActionResult<IList<CardDTO>>> TakeCards(int user_id)
        {
            IList<CardDTO> cardDTOs = await this._service.GetCardsByUserId(user_id);

            return Ok(cardDTOs);
        }

        [HttpGet("{card-id}")]
        public async Task<ActionResult<CardDTO>> GetCardById(int card_id)
        {
            CardDTO cardDTO = await this._service.GetCardData(card_id);

            return Ok(cardDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CardDTO>> AddNewCard(int user_id, [FromBody] CardDTO cardDTO)
        {
            await this._service.CreateCard(user_id, cardDTO);

            int cardId = (await this._service.GetCardsByUserId(user_id)).Max(x => x.SubstanceId);

            CardDTO card = await this._service.GetCardData(cardId);

            return card;
        }

        [HttpPut]
        public async Task<ActionResult> EditCardEnabled(int card_id, bool enabled)
        {
            await this._service.EditCardEnabled(card_id, enabled);

            return NoContent();
        }
    }
}
