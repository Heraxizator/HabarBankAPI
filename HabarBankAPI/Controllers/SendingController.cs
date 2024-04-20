using Asp.Versioning;
using AutoMapper;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using HabarBankAPI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/transfers/")]
    public class SendingController : ControllerBase
    {
        private readonly ISendingService _sending_service;
        private readonly ISecurityService _security_service;

        public SendingController()
        {
            ApplicationDbContext context = new();

            GenericRepository<Sending> sendingRepositoty = new(context);

            GenericRepository<Card> cardsRepository = new(context);

            GenericRepository<User> usersRepository = new(context);

            GenericRepository<OperationType> operationTypesRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<SendingDTO, Sending>.MapperA;

            Mapper mapperB = AbstractMapper<Sending, SendingDTO>.MapperA;

            _sending_service = new SendingService(mapperA, mapperB, sendingRepositoty, cardsRepository, usersRepository, operationTypesRepository, unitOfWork);

            _security_service = ServiceLocator.Instance.GetService<ISecurityService>();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("transfer-id")]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransferByTransferId(long transfer_id, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                SendingDTO sendingDTO = await _sending_service.GetTransferByTransferId(transfer_id);

                return Ok(sendingDTO);
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet()]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransfersByEntityId(long card_id, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                IList<SendingDTO> sendingDTOs = await _sending_service.GetTransfersBySubstanceId(card_id);

                return Ok(sendingDTOs);
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [MapToApiVersion("2.0")]
        [HttpGet("transfers")]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransfersByEntityId2(long card_id, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                IList<SendingDTO> sendingDTOs = await _sending_service.GetTransfersBySubstanceId(card_id);

                return Ok(sendingDTOs);
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [MapToApiVersion("2.0")]
        [HttpGet("enrollments")]
        public async Task<ActionResult<IList<SendingDTO>>> GetEnrollmentsByEntityId(long card_id, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                IList<SendingDTO> sendingDTOs = await _sending_service.GetEnrollmentsBySubstanceId(card_id);

                return Ok(sendingDTOs);
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<ActionResult<SendingDTO>> AddNewTransfer([FromBody] SendingDTO sendingDTO, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                await _sending_service.CreateTransfer(sendingDTO);

                long sendingId = (await _sending_service.GetTransfersBySubstanceId(sendingDTO.SubstanceId)).Max(x => x.SendingId);

                SendingDTO sending = await _sending_service.GetTransferByTransferId(sendingId);

                return sending;
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }

        [MapToApiVersion("1.0")]
        [HttpPut]
        public async Task<ActionResult> SetTransferEnabled(long sending_id, bool enabled, [FromHeader] string token)
        {
            try
            {
                await _security_service.IsExists(token);

                await _sending_service.SetTransferStatus(sending_id, enabled);

                return NoContent();
            }

            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
