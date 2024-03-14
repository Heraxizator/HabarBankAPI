using AutoMapper;
using HabarBankAPI.Application;
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

            IGenericRepository<OperationType> repository = new GenericRepository<OperationType>(context);

            UnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<OperationTypeDTO, OperationType>.MapperA;

            Mapper mapperB = AbstractMapper<OperationTypeDTO, OperationType>.MapperB;

            this._service = new ActionTypeService(repository, unitOfWork, mapperA, mapperB);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetAllActionTypes()
        {
            IList<OperationTypeDTO> actionTypeDTOs = await _service.GetAllActionTypes();

            return Ok(actionTypeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> GetActionTypeById(int id)
        {
            OperationTypeDTO actionTypeDTO = await _service.GetActionTypeById(id);

            return Ok(actionTypeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<OperationTypeDTO>> AddActionType([FromBody] OperationTypeDTO actionTypeDTO)
        {
            await _service.CreateNewActionType(actionTypeDTO);

            long actionTypeId = (await this._service.GetAllActionTypes()).Max(x => x.OperationTypeId);

            OperationTypeDTO actionType = await this._service.GetActionTypeById(actionTypeId);

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
