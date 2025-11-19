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

public sealed class ValutaRateService(IDbContext context) : IValutaRateService
{
    private readonly IGenericRepository<ValutaRate> _repository = new GenericRepository<ValutaRate>(context);

    public Task<ValutaRate> CreateAsync(CreateValutaRateRequest request, CancellationToken cancellationToken)
    {
        return _repository.CreateAsync(ValutaRateMapper.GetModel(request.Body), cancellationToken);
    }

    public async Task DeleteAsync(DeleteValutaRateRequest request, CancellationToken cancellationToken)
    {
        ValutaRate? valutaRate = await _repository.FindByIdAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(valutaRate);

        await _repository.RemoveAsync(valutaRate, cancellationToken);
    }

    public Task<IEnumerable<ValutaRate>> GetAsync(GetValutaRateRequest request, CancellationToken cancellationToken)
    {
        return _repository.GetAsync(x => true, request.Page, request.PerInPage, cancellationToken);
    }

    public Task UpdateAsync(UpdateValutaRateRequest request, CancellationToken cancellationToken)
    {
        return _repository.UpdateAsync(ValutaRateMapper.GetModel(request.Body), cancellationToken);
    }
}
