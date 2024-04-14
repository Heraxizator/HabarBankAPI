using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/card-types")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CardTypeController : ControllerBase
    {
        private readonly CardTypeService _service;


        public CardTypeController()
        {
            ApplicationDbContext context = new();

            IGenericRepository<CardType> repository = new GenericRepository<CardType>(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<CardTypeDTO, CardType>.MapperA;

            Mapper mapperB = AbstractMapper<CardTypeDTO, CardType>.MapperB;

            _service = new CardTypeService(repository, unitOfWork, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardTypeDTO>>> GetAllCardTypes()
        {
            try
            {
                IList<CardTypeDTO> cardTypes = await _service.GetAllCardTypes();

                return Ok(cardTypes);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardTypeDTO>> GetCardTypeById(int id)
        {
            try
            {
                CardTypeDTO cardTypeDTO = await _service.GetCardTypeById(id);

                return Ok(cardTypeDTO);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CardTypeDTO>> AddNewCardType([FromBody] CardTypeDTO cardTypeDTO)
        {
            try
            {
                await _service.CreateNewCardType(cardTypeDTO);

                long cardTypeId = (await this._service.GetAllCardTypes()).Max(x => x.CardTypeId);

                CardTypeDTO cardType = await this._service.GetCardTypeById(cardTypeId);

                return cardType;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCardTypeEnabled(int id, bool enabled)
        {
            try
            {
                await _service.SetCardTypeEnabled(id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
