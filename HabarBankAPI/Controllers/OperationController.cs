using AutoMapper;
using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Action = HabarBankAPI.Domain.Entities.Operation.Action;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/operations/")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ActionService _service;

        public OperationController()
        {
            ApplicationDbContext context = new();

            GenericRepository<Action> repository = new(context);

            Mapper mapperA = AbstractMapper<ActionDTO, Action>.MapperA;

            Mapper mapperB = AbstractMapper<Action, ActionDTO>.MapperA;

            this._service = new ActionService(repository, mapperA, mapperB);
        }

        [HttpGet("{action-id}")]
        public async Task<ActionResult<IList<ActionDTO>>> GetOperationByOperationId(int action_id)
        {
            ActionDTO actionDTO = await this._service.GetActionByActionId(action_id);

            return Ok(actionDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IList<ActionDTO>>> GetOperationsByEntityId(long card_id)
        {
            IList<ActionDTO> actionDTOs = await this._service.GetActionsByEntityId(card_id);

            return Ok(actionDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<ActionDTO>> AddNewOperation([FromBody] ActionDTO actionDTO)
        {
            await this._service.CreateNewAction(actionDTO);

            long actionId = (await this._service.GetAllActions(int.MaxValue)).Max(x => x.ActionId);

            ActionDTO action = await this._service.GetActionByActionId(actionId);

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
