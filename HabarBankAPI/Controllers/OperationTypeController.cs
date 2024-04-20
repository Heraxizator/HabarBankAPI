using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application;
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
    [Route("api/{version:apiVersion}/operation-types")]
    [ApiVersion("1.0")]
    [ApiController]
    public class OperationTypeController : ControllerBase
    {
        private readonly IActionTypeService _operationtype_service;
        private readonly ISecurityService _security_service;

        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public OperationTypeController() 
        {
            ApplicationDbContext context = new();

            IGenericRepository<OperationType> repository = new GenericRepository<OperationType>(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<OperationTypeDTO, OperationType>.MapperA;

            Mapper mapperB = AbstractMapper<OperationTypeDTO, OperationType>.MapperB;

            this._operationtype_service = new ActionTypeService(repository, unitOfWork, mapperA, mapperB);

            this._security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetAllActionTypes([FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                IList<OperationTypeDTO> actionTypeDTOs = await _operationtype_service.GetAllActionTypes();

                return Ok(actionTypeDTOs);
            }
            
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> GetActionTypeById(int id, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                OperationTypeDTO actionTypeDTO = await _operationtype_service.GetActionTypeById(id);

                return Ok(actionTypeDTO);
            }
            
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OperationTypeDTO>> AddActionType([FromBody] OperationTypeDTO actionTypeDTO, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await _operationtype_service.CreateNewActionType(actionTypeDTO);

                long actionTypeId = (await this._operationtype_service.GetAllActionTypes()).Max(x => x.OperationTypeId);

                OperationTypeDTO actionType = await this._operationtype_service.GetActionTypeById(actionTypeId);

                return actionType;
            }
            
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditActionTypeEnabled(int id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await this._security_service.IsExists(token);

                await _operationtype_service.SetActionTypeEnabled(id, enabled);

                return NoContent();
            }
            
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
