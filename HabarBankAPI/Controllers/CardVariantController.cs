using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.Card;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/card-variants")]
    [ApiController]
    public class CardVariantController : ControllerBase
    {
        private readonly CardVariantService _service;

        public CardVariantController()
        {
            ApplicationDbContext context = new();

            IGenericRepository<CardVariant> repository = new GenericRepository<CardVariant>(context);

            Mapper mapperA = AbstractMapper<CardVariantDTO, CardVariant>.MapperA;

            Mapper mapperB = AbstractMapper<CardVariantDTO, CardVariant>.MapperB;

            this._service = new CardVariantService(repository, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardVariantDTO>>> GetAllCardVariants()
        {
            IList<CardVariantDTO> cardVariantDTOs = await _service.GetAllCardVariants();

            return Ok(cardVariantDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardVariantDTO>> GetCardVariantById(int id)
        {
            CardVariantDTO cardVariantDTO = await _service.GetCardVariantById(id);

            return Ok(cardVariantDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CardVariantDTO>> AddNewCardVariant([FromBody] CardVariantDTO cardVariantDTO)
        {
            await _service.CreateNewCardVariant(cardVariantDTO);

            long cardVariantId = (await this._service.GetAllCardVariants()).Max(x => x.CardVariantId);

            CardVariantDTO cardVariant = await this._service.GetCardVariantById(cardVariantId);

            return cardVariant;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCardVariantEnabled(int id, bool enabled)
        {
            await _service.SetCardVariantEnabled(id, enabled);

            return NoContent();
        }
    }
}
