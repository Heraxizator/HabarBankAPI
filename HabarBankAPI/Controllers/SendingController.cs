﻿using AutoMapper;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.AspNetCore.Mvc;

namespace HabarBankAPI.Web.Controllers
{
    [Route("api/transfers/")]
    [ApiController]
    public class SendingController : ControllerBase
    {
        private readonly SendingService _service;

        public SendingController()
        {
            ApplicationDbContext context = new();

            GenericRepository<Sending> sendingRepositoty = new(context);

            GenericRepository<Card> cardsRepository = new(context);

            GenericRepository<User> usersRepository = new(context);

            GenericRepository<OperationType> operationTypesRepository = new(context);

            UnitOfWork unitOfWork = new(context);

            Mapper mapperA = AbstractMapper<SendingDTO, Sending>.MapperA;

            Mapper mapperB = AbstractMapper<Sending, SendingDTO>.MapperA;

            this._service = new SendingService(mapperA, mapperB, sendingRepositoty, cardsRepository, usersRepository, operationTypesRepository, unitOfWork);
        }


        [HttpGet("{transfer-id}")]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransferByTransferId(long transfer_id)
        {
            SendingDTO sendingDTO = await this._service.GetTransferByTransferId(transfer_id);

            return Ok(sendingDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IList<SendingDTO>>> GetTransfersByEntityId(long card_id)
        {
            IList<SendingDTO> sendingDTOs = await this._service.GetTransfersBySubstanceId(card_id);

            return Ok(sendingDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<SendingDTO>> AddNewTransfer([FromBody] SendingDTO sendingDTO)
        {
            await this._service.CreateTransfer(sendingDTO);

            long sendingId = (await this._service.GetTransfersBySubstanceId(sendingDTO.SubstanceId)).Max(x => x.SendingId);

            SendingDTO sending = await this._service.GetTransferByTransferId(sendingId);

            return sending;
        }

        [HttpPut]
        public async Task<ActionResult> SetTransferEnabled(long sending_id, bool enabled)
        {
            await this._service.SetTransferStatus(sending_id, enabled);

            return NoContent();
        }
    }
}
