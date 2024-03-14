using AutoMapper;
using HabarBankAPI.Application;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/operations/")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly OperationService _service;

        public OperationController()
        {
            ApplicationDbContext context = new();

            GenericRepository<Operation> actionsRepository = new(context);

            GenericRepository<OperationType> actionTypesRepository = new(context);

            GenericRepository<Card> cardsRepository = new(context);

            UnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<OperationDTO, Operation>.MapperA;

            Mapper mapperB = AbstractMapper<Operation, OperationDTO>.MapperA;

            this._service = new OperationService(actionsRepository, actionTypesRepository, cardsRepository, unitOfWork, mapperA, mapperB);
        }

        [HttpGet("{action-id}")]
        public async Task<ActionResult<IList<OperationDTO>>> GetOperationByOperationId(int action_id)
        {
            OperationDTO actionDTO = await this._service.GetActionByActionId(action_id);

            return Ok(actionDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IList<OperationDTO>>> GetOperationsByEntityId(long card_id)
        {
            IList<OperationDTO> actionDTOs = await this._service.GetActionsByEntityId(card_id);

            return Ok(actionDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> AddNewOperation([FromBody] OperationDTO actionDTO)
        {
            await this._service.CreateNewAction(actionDTO);

            long actionId = (await this._service.GetAllActions(int.MaxValue)).Max(x => x.OperationId);

            OperationDTO action = await this._service.GetActionByActionId(actionId);

            return action;
        }

        [HttpPut]
        public async Task<ActionResult> SetTOperationEnabled(int action_id, bool enabled)
        {
            await this._service.SetActionEnabled(action_id, enabled);

            return NoContent();
        }
    }
}
