using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
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
    [Route("api/card-variants")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CardVariantController : ControllerBase
    {
        private readonly CardVariantService _service;

        public CardVariantController()
        {
            ApplicationDbContext context = new();

            GenericRepository<CardVariant> cardVariantsRepository = new(context);

            GenericRepository<CardType> cardTypesRepository = new(context);

            GenericRepository<UserLevel> userLevelsRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<CardVariantDTO, CardVariant>.MapperA;

            Mapper mapperB = AbstractMapper<CardVariantDTO, CardVariant>.MapperB;

            this._service = new CardVariantService(cardVariantsRepository, cardTypesRepository, userLevelsRepository, unitOfWork, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardVariantDTO>>> GetAllCardVariants()
        {
            try
            {
                IList<CardVariantDTO> cardVariantDTOs = await _service.GetAllCardVariants();

                return Ok(cardVariantDTOs);
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardVariantDTO>> GetCardVariantById(int id)
        {
            try
            {
                CardVariantDTO cardVariantDTO = await _service.GetCardVariantById(id);

                return Ok(cardVariantDTO);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CardVariantDTO>> AddNewCardVariant([FromBody] CardVariantDTO cardVariantDTO)
        {
            try
            {
                await _service.CreateNewCardVariant(cardVariantDTO);

                long cardVariantId = (await this._service.GetAllCardVariants()).Max(x => x.CardVariantId);

                CardVariantDTO cardVariant = await this._service.GetCardVariantById(cardVariantId);

                return cardVariant;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCardVariantEnabled(int id, bool enabled)
        {
            try
            {
                await _service.SetCardVariantEnabled(id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
