using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/card-types")]
    [ApiController]
    public class CardTypeController : ControllerBase
    {
        private readonly CardTypeService _service;


        public CardTypeController()
        {
            ApplicationDbContext context = new();

            IGenericRepository<CardType> repository = new GenericRepository<CardType>(context);

            Mapper mapperA = AbstractMapper<CardTypeDTO, CardType>.MapperA;

            Mapper mapperB = AbstractMapper<CardTypeDTO, CardType>.MapperB;

            _service = new CardTypeService(repository, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardTypeDTO>>> GetAllCardTypes()
        {
            IList<CardTypeDTO> cardTypes = await _service.GetAllCardTypes();

            return Ok(cardTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardTypeDTO>> GetCardTypeById(int id)
        {
            CardTypeDTO cardTypeDTO = await _service.GetCardTypeById(id);

            return Ok(cardTypeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CardTypeDTO>> AddNewCardType([FromBody] CardTypeDTO cardTypeDTO)
        {
            await _service.CreateNewCardType(cardTypeDTO);

            int cardTypeId = (await this._service.GetAllCardTypes()).Max(x => x.CardTypeId);

            CardTypeDTO cardType = await this._service.GetCardTypeById(cardTypeId);

            return cardType;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCardTypeEnabled(int id, bool enabled)
        {
            await _service.SetCardTypeEnabled(id, enabled);

            return NoContent();
        }
    }
}
