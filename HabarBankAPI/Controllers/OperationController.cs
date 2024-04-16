using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/{version:apiVersion}/operations/")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operation_service;
        private readonly ISecurityService _security_service;

        public OperationController()
        {
            ApplicationDbContext context = new();

            GenericRepository<Operation> actionsRepository = new(context);

            GenericRepository<OperationType> actionTypesRepository = new(context);

            GenericRepository<Card> cardsRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<OperationDTO, Operation>.MapperA;

            Mapper mapperB = AbstractMapper<Operation, OperationDTO>.MapperA;

            this._operation_service = new OperationService(actionsRepository, actionTypesRepository, cardsRepository, unitOfWork, mapperA, mapperB);

            this._security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet("{action-id}")]
        public async Task<ActionResult<IList<OperationDTO>>> GetOperationByOperationId(int action_id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                OperationDTO actionDTO = await this._operation_service.GetActionByActionId(action_id);

                return Ok(actionDTO);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<OperationDTO>>> GetOperationsByEntityId(long card_id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<OperationDTO> actionDTOs = await this._operation_service.GetActionsByEntityId(card_id);

                return Ok(actionDTOs);
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OperationDTO>> AddNewOperation([FromBody] OperationDTO actionDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._operation_service.CreateNewAction(actionDTO);

                long actionId = (await this._operation_service.GetAllActions(int.MaxValue)).Max(x => x.OperationId);

                OperationDTO action = await this._operation_service.GetActionByActionId(actionId);

                return action;
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> SetTOperationEnabled(int action_id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await this._operation_service.SetActionEnabled(action_id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
