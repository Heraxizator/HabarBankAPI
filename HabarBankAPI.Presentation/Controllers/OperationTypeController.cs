using AutoMapper;
using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly ActionTypeService _service;
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public OperationTypeController() 
        {
            ApplicationDbContext context = new();

            IGenericRepository<ActionType> repository = new GenericRepository<ActionType>(context);

            Mapper mapperA = AbstractMapper<ActionTypeDTO, ActionType>.MapperA;

            Mapper mapperB = AbstractMapper<ActionTypeDTO, ActionType>.MapperB;

            this._service = new ActionTypeService(repository, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActionTypeDTO>>> GetAllActionTypes()
        {
            IList<ActionTypeDTO> actionTypeDTOs = await _service.GetAllActionTypes();

            return Ok(actionTypeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionTypeDTO>> GetActionTypeById(int id)
        {
            ActionTypeDTO actionTypeDTO = await _service.GetActionTypeById(id);

            return Ok(actionTypeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ActionTypeDTO>> AddActionType([FromBody] ActionTypeDTO actionTypeDTO)
        {
            await _service.CreateNewActionType(actionTypeDTO);

            int actionTypeId = (await this._service.GetAllActionTypes()).Max(x => x.ActionTypeId);

            ActionTypeDTO actionType = await this._service.GetActionTypeById(actionTypeId);

            return actionType;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditActionTypeEnabled(int id, bool enabled)
        {
            await _service.SetActionTypeEnabled(id, enabled);

            return NoContent();
        }
    }
}
