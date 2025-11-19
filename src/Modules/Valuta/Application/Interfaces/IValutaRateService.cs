using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Requests;
using Valutas.Domain.Entities;

namespace Valutas.Application.Interfaces;

public interface IValutaRateService
{
    Task<IEnumerable<ValutaRate>> GetAsync(GetValutaRateRequest request, CancellationToken cancellationToken);
    Task<ValutaRate> CreateAsync(CreateValutaRateRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateValutaRateRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteValutaRateRequest request, CancellationToken cancellationToken);
}
