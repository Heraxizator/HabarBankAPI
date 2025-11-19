using Common.Abstracts;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Requests;
using Valutas.Application.Interfaces;
using Valutas.Application.Mappers;
using Valutas.Domain.Entities;

namespace Valutas.Application.Services;

public sealed class ValutaService(IDbContext context) : IValutaService
{
    private readonly IGenericRepository<Valuta> _repository = new GenericRepository<Valuta>(context);

    public Task<Valuta> CreateAsync(CreateValutaRequest request, CancellationToken cancellationToken)
    {
        return _repository.CreateAsync(ValutaMapper.GetModel(request.Body), cancellationToken);
    }

    public async Task DeleteAsync(DeleteValutaRequest request, CancellationToken cancellationToken)
    {
        Valuta? Valuta = await _repository.FindByIdAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(Valuta);

        await _repository.RemoveAsync(Valuta, cancellationToken);
    }

    public Task<IEnumerable<Valuta>> GetAsync(GetValutaRequest request, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(x => true, request.Page, request.PerInPage, cancellationToken);
    }

    public Task UpdateAsync(UpdateValutaRequest request, CancellationToken cancellationToken)
    {
        return _repository.UpdateAsync(ValutaMapper.GetModel(request.Body), cancellationToken);
    }
}
