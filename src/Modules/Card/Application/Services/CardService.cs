using Card.Application.DTOs.Requests;
using Card.Application.Interfaces;
using Card.Application.Mappers;
using Common.Abstracts;
using Common.Constants;
using Common.DTOs;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Application.Services;

public sealed class CardService(IDbContext context) : ICardService
{
    public IGenericRepository<Domain.Entities.Card> _repository = new GenericRepository<Domain.Entities.Card>(context);

    public Task<Domain.Entities.Card> CreateAsync(CreateCardRequest request, CancellationToken cancellationToken)
    {
        return _repository.CreateAsync(CardMapper.GetModel(request.Body), cancellationToken);
    }

    public async Task DeleteAsync(DeleteCardRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Card? card = await _repository.FindByIdAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(card);

        await _repository.RemoveAsync(card, cancellationToken);
    }

    public async Task<Domain.Entities.Card?> GetAsync(GetCardRequest request, CancellationToken cancellationToken)
    {
        return await _repository.FindByIdAsync(request.Id, cancellationToken);
    }

    public Task UpdateAsync(UpdateCardRequest request, CancellationToken cancellationToken)
    {
        return _repository.UpdateAsync(CardMapper.GetModel(request.Body), cancellationToken);
    }
}
