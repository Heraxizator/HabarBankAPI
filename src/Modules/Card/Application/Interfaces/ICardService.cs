using Card.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Application.Interfaces;

public interface ICardService
{
    Task<Domain.Entities.Card> CreateAsync(CreateCardRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteCardRequest request, CancellationToken cancellationToken);
    Task<Domain.Entities.Card?> GetAsync(GetCardRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateCardRequest request, CancellationToken cancellationToken);
}
