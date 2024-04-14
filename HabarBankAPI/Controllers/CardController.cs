using AutoMapper;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Infrastructure.Uow;
using Asp.Versioning;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Share;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/cards/")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CardController : ControllerBase
    {

        private readonly GenericRepository<Card> _cards_repository;
        private readonly GenericRepository<User> _users_repository;
        private readonly GenericRepository<Substance> _entities_repository;
        private readonly GenericRepository<CardVariant> _cardvariant_repository;

        private readonly CardService _card_service;
        private readonly SecurityService _security_service;

        private readonly Mapper _card_mapperA;
        private readonly Mapper _card_mapperB;
        private readonly Mapper _user_mapper;

        public CardController()
        {
            ApplicationDbContext context = new();

            this._card_mapperA = AbstractMapper<CardDTO, Card>.MapperA;

            this._card_mapperB = AbstractMapper<Card, CardDTO>.MapperA;

            this._user_mapper = AbstractMapper<AccountDTO, Account>.MapperA;

            this._cards_repository = new GenericRepository<Card>(context);

            this._users_repository = new GenericRepository<User>(context);

            this._entities_repository = new GenericRepository<Substance>(context);

            this._cardvariant_repository = new GenericRepository<CardVariant>(context);

            AppUnitOfWork unitOfWork = new(context);

            this._card_service = new CardService(
                _card_mapperA, _card_mapperB, _user_mapper, _cards_repository, _users_repository, _entities_repository, _cardvariant_repository, unitOfWork);

            SecurityDbContext securityDbContext = new();

            SecurityUnitOfWork securityUnitOfWork = new(securityDbContext);

            GenericRepository<Security> repository = new(securityDbContext);

            this._security_service = new SecurityService(repository, securityUnitOfWork);
        }

        [HttpGet]
        public async Task<ActionResult<IList<CardDTO>>> TakeCards(long user_id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<CardDTO> cardDTOs = await this._card_service.GetCardsByUserId(user_id);

                return Ok(cardDTOs);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{card-id}")]
        public async Task<ActionResult<CardDTO>> GetCardById(long card_id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                CardDTO cardDTO = await this._card_service.GetCardData(card_id);

                return Ok(cardDTO);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CardDTO>> AddNewCard(long user_id, [FromBody] CardDTO cardDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._card_service.CreateCard(user_id, cardDTO);

                long cardId = (await this._card_service.GetCardsByUserId(user_id)).Max(x => x.CardId);

                CardDTO card = await this._card_service.GetCardData(cardId);

                return card;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditCardEnabled(long card_id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._card_service.EditCardEnabled(card_id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
