using Cards.Application.DTOs.Base;
using Cards.Application.DTOs.Requests;
using Cards.Application.Interfaces;
using Cards.Application.Mappers;
using Cards.Domain.Entities;
using Common.Abstracts;
using Common.Exceptions;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using Operations.Application.DTOs.Base;
using Operations.Application.DTOs.Requests;
using Operations.Application.Interfaces;
using Operations.Application.Mappers;
using Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.Services;

public sealed class OperationService(IDbContext context, ICardService service) : IOperationService
{
    public readonly IGenericRepository<Operation> _repository = new GenericRepository<Operation>(context);
    public readonly ICardService _service = service;

    public async Task<Operation> CreateAsync(CreateOperationRequest request, CancellationToken cancellationToken)
    {
        Operation operation = OperationMapper.GetModel(request.Body);
        
        Card? cardRecipient = await _service.GetAsync(operation.CardRecipientId, cancellationToken);
        Card? cardSender = await _service.GetAsync(operation.CardSenderId, cancellationToken);

        ArgumentNullException.ThrowIfNull(cardRecipient);
        ArgumentNullException.ThrowIfNull(cardSender);

        if (cardRecipient.ValutaId != cardSender.ValutaId)
        {
            throw new DomainException("Карты поддерживают разные валюты");
        }

        cardRecipient.Score += operation.Score;
        cardSender.Score -= operation.Score;

        CardBody cardRecipientBody = CardMapper.GetBody(cardRecipient);
        CardBody cardSenderBody = CardMapper.GetBody(cardSender);

        await _service.UpdateAsync(new UpdateCardRequest(cardRecipientBody), cancellationToken);
        await _service.UpdateAsync(new UpdateCardRequest(cardSenderBody), cancellationToken);

        return await _repository.CreateAsync(operation, cancellationToken);
    }

    public async Task DeleteAsync(DeleteOperationRequest request, CancellationToken cancellationToken)
    {
        Operation? operation = await _repository.FindByIdAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(operation);

        Card? cardRecipient = await _service.GetAsync(operation.CardRecipientId, cancellationToken);
        Card? cardSender = await _service.GetAsync(operation.CardSenderId, cancellationToken);

        ArgumentNullException.ThrowIfNull(cardRecipient);
        ArgumentNullException.ThrowIfNull(cardSender);

        cardRecipient.Score -= operation.Score;
        cardSender.Score += operation.Score;

        CardBody cardRecipientBody = CardMapper.GetBody(cardRecipient);
        CardBody cardSenderBody = CardMapper.GetBody(cardSender);

        await _service.UpdateAsync(new UpdateCardRequest(cardRecipientBody), cancellationToken);
        await _service.UpdateAsync(new UpdateCardRequest(cardSenderBody), cancellationToken);
    }

    public Task<IEnumerable<Operation>> GetAsync(GetOperationsRequest request, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(
            x => x.CardSenderId == request.CardId || x.CardRecipientId == request.CardId, request.Page, request.PerInPage, cancellationToken);
    }

    public Task UpdateAsync(UpdateOperationRequest request, CancellationToken cancellationToken)
    {
        return _repository.UpdateAsync(OperationMapper.GetModel(request.Body), cancellationToken);
    }
}
