using AutoMapper;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/users/")]
    [ApiController]
    public class SendingController : ControllerBase
    {
        private readonly SendingService _service;

        public SendingController()
        {
            ApplicationDbContext context = new();

            GenericRepository<Sending> sendingRepositoty = new(context);

            GenericRepository<Substance> substanceRepository = new(context);

            Mapper mapperA = AbstractMapper<SendingDTO, Sending>.MapperA;

            Mapper mapperB = AbstractMapper<Sending, SendingDTO>.MapperA;

            this._service = new SendingService(mapperA, mapperB, sendingRepositoty, substanceRepository);
        }


        [HttpGet("{user-id}/cards/{card-id}/sendings/{sending-id}")]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransferByTransferId(int sending_id)
        {
            SendingDTO sendingDTO = await this._service.GetTransferByTransferId(sending_id);

            return Ok(sendingDTO);
        }

        [HttpGet("{user-id}/cards/{card-id}/sendings")]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransfersByEntityId(int card_id)
        {
            IList<SendingDTO> sendingDTOs = await this._service.GetTransfersBySubstanceId(card_id);

            return Ok(sendingDTOs);
        }

        [HttpPost("{user-id}/cards/{card-id}/sendings/")]
        public async Task<ActionResult<SendingDTO>> AddNewTransfer([FromBody] SendingDTO sendingDTO)
        {
            await this._service.CreateTransfer(sendingDTO);

            int sendingId = (await this._service.GetTransfersBySubstanceId(sendingDTO.SubstanceId)).Max(x => x.ActionId);

            SendingDTO sending = await this._service.GetTransferByTransferId(sendingId);

            return sending;
        }

        [HttpPut("{user-id}/cards/{card-id}/sendings/")]
        public async Task<ActionResult> SetTransferEnabled(int sending_id, bool enabled)
        {
            await this._service.SetTransferStatus(sending_id, enabled);

            return NoContent();
        }
    }
}
