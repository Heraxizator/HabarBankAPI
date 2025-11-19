using Cards.Application.DTOs.Requests;
using Cards.Application.Interfaces;
using Cards.Application.Mappers;
using Cards.Domain.Entities;
using Cards.Domain.Enums;
using Common.Abstracts;
using Common.Constants;
using Common.DTOs;
using Common.Exceptions;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Services;

public sealed class CardService(IDbContext context) : ICardService
{
    public IGenericRepository<Card> _repository = new GenericRepository<Card>(context);

    public Task<Card> CreateAsync(CreateCardRequest request, CancellationToken cancellationToken)
    {
        if (request.Body.TypeId > (int)Enum.GetValues(typeof(CardTypes)).Cast<CardTypes>().Last())
        {
            throw new DomainException("Указанный TypeId не существует");
        }

        return _repository.CreateAsync(CardMapper.GetModel(request.Body), cancellationToken);
    }

    public async Task DeleteAsync(DeleteCardRequest request, CancellationToken cancellationToken)
    {
        Card? card = await _repository.FindByIdAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(card);

        await _repository.RemoveAsync(card, cancellationToken);
    }

    public Task<IEnumerable<Card>> GetAsync(GetCardsRequest request, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(x => x.UserId == request.UserId, cancellationToken);
    }

    public async Task<Card?> GetAsync(long id, CancellationToken cancellationToken)
    {
        return await _repository.FindByIdAsync(id, cancellationToken);
    }

    public Task UpdateAsync(UpdateCardRequest request, CancellationToken cancellationToken)
    {
        return _repository.UpdateAsync(CardMapper.GetModel(request.Body), cancellationToken);
    }
}
