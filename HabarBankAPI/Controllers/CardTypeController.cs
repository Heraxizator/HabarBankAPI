using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/{version:apiVersion}/card-types")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CardTypeController : ControllerBase
    {
        private readonly ICardTypeService _cardtype_service;
        private readonly ISecurityService _security_service;

        public CardTypeController()
        {
            ApplicationDbContext context = new();

            IGenericRepository<CardType> repository = new GenericRepository<CardType>(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<CardTypeDTO, CardType>.MapperA;

            Mapper mapperB = AbstractMapper<CardTypeDTO, CardType>.MapperB;

            this._cardtype_service = new CardTypeService(repository, unitOfWork, mapperA, mapperB);

            this._security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardTypeDTO>>> GetAllCardTypes([FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<CardTypeDTO> cardTypes = await this._cardtype_service.GetAllCardTypes();

                return Ok(cardTypes);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardTypeDTO>> GetCardTypeById(int id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                CardTypeDTO cardTypeDTO = await this._cardtype_service.GetCardTypeById(id);

                return Ok(cardTypeDTO);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CardTypeDTO>> AddNewCardType([FromBody] CardTypeDTO cardTypeDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await _cardtype_service.CreateNewCardType(cardTypeDTO);

                long cardTypeId = (await this._cardtype_service.GetAllCardTypes()).Max(x => x.CardTypeId);

                CardTypeDTO cardType = await this._cardtype_service.GetCardTypeById(cardTypeId);

                return cardType;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCardTypeEnabled(int id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await _cardtype_service.SetCardTypeEnabled(id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
