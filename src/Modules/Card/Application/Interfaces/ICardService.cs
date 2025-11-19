using Cards.Application.DTOs.Requests;
using Cards.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Interfaces;

public interface ICardService
{
    Task<Card> CreateAsync(CreateCardRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteCardRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<Card>> GetAsync(GetCardsRequest request, CancellationToken cancellationToken);
    Task<Card?> GetAsync(long id, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateCardRequest request, CancellationToken cancellationToken);
}
