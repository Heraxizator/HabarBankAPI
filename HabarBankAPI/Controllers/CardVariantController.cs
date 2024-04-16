using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
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
    [Route("api/{version:apiVersion}/card-variants")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CardVariantController : ControllerBase
    {
        private readonly ICardVariantService _cardvariant_service;
        private readonly ISecurityService _security_service;

        public CardVariantController()
        {
            ApplicationDbContext context = new();

            GenericRepository<CardVariant> cardVariantsRepository = new(context);

            GenericRepository<CardType> cardTypesRepository = new(context);

            GenericRepository<UserLevel> userLevelsRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<CardVariantDTO, CardVariant>.MapperA;

            Mapper mapperB = AbstractMapper<CardVariantDTO, CardVariant>.MapperB;

            this._cardvariant_service = new CardVariantService(cardVariantsRepository, cardTypesRepository, userLevelsRepository, unitOfWork, mapperA, mapperB);

            this._security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardVariantDTO>>> GetAllCardVariants([FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                IList<CardVariantDTO> cardVariantDTOs = await _cardvariant_service.GetAllCardVariants();

                return Ok(cardVariantDTOs);
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardVariantDTO>> GetCardVariantById(int id, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                CardVariantDTO cardVariantDTO = await _cardvariant_service.GetCardVariantById(id);

                return Ok(cardVariantDTO);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CardVariantDTO>> AddNewCardVariant([FromBody] CardVariantDTO cardVariantDTO, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                await _cardvariant_service.CreateNewCardVariant(cardVariantDTO);

                long cardVariantId = (await this._cardvariant_service.GetAllCardVariants()).Max(x => x.CardVariantId);

                CardVariantDTO cardVariant = await this._cardvariant_service.GetCardVariantById(cardVariantId);

                return cardVariant;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCardVariantEnabled(int id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                await _cardvariant_service.SetCardVariantEnabled(id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
