using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;
using Valutas.Application.DTOs.Requests;
using Valutas.Domain.Entities;

namespace Valutas.Application.Interfaces;

public interface IValutaService
{
    Task<IEnumerable<Valuta>> GetAsync(GetValutaRequest request, CancellationToken cancellationToken);
    Task<Valuta> CreateAsync(CreateValutaRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateValutaRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteValutaRequest request, CancellationToken cancellationToken);
}
