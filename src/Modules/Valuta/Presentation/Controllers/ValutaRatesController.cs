using Common.Abstracts;
using Common.Constants;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;
using Valutas.Application.DTOs.Requests;
using Valutas.Application.DTOs.Responses;
using Valutas.Application.Interfaces;
using Valutas.Application.Mappers;
using Valutas.Domain.Entities;

namespace Valutas.Presentation.Controllers;

[ApiController]
[Route("api/v1/valutas/rates")]

public class ValutaRatesController : BaseController
{
    private readonly IValutaRateService _service;
    public ValutaRatesController(IValutaRateService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerResponse(200, "Список валют", typeof(IEnumerable<ValutaRateBody>))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ValutaRateBody>>>> GetAsync(int perInPage = 20, int page = 1)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        GetValutaRateRequest request = new(perInPage, page);

        if (!ValidateRequest<GetValutaRateRequest, IEnumerable<ValutaRateBody>>(request, out var validationError))
            return validationError!;

        IEnumerable<ValutaRate> ValutaRates = await _service.GetAsync(request, cancellationTokenSource.Token);

        return Success(ValutaRates.Select(ValutaRateMapper.GetBody), "ValutaRates retrieved successfully");
    }

    [HttpPost]
    [SwaggerResponse(200, "Валюта успешно создана", typeof(CreateValutaRateResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<CreateValutaRateResponse>>> PostAsync([FromBody] CreateValutaRateRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<CreateValutaRateRequest, CreateValutaRateResponse>(request, out var validationError))
            return validationError!;

        ValutaRate? ValutaRate = await _service.CreateAsync(request, cancellationTokenSource.Token);

        if (ValutaRate is null)
        {
            return NotFound<CreateValutaRateResponse>();
        }

        ValutaRateBody ValutaRateBody = ValutaRateMapper.GetBody(ValutaRate);

        return Success(new CreateValutaRateResponse(ValutaRateBody), "ValutaRate created successfully");
    }

    [HttpPut]
    [SwaggerResponse(200, "Валюта успешно обновлена", typeof(UpdateValutaRateResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<UpdateValutaRateResponse>>> PutAsync([FromBody] UpdateValutaRateRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<UpdateValutaRateRequest, UpdateValutaRateResponse>(request, out var validationError))
            return validationError!;

        await _service.UpdateAsync(request, cancellationTokenSource.Token);

        return Success(new UpdateValutaRateResponse(), "ValutaRate updated successfully");
    }

    [HttpDelete]
    [SwaggerResponse(200, "Валюта успешно удалена", typeof(DeleteValutaRateResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<DeleteValutaRateResponse>>> DeleteAsync(long id)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        DeleteValutaRateRequest request = new(id);

        if (!ValidateRequest<DeleteValutaRateRequest, DeleteValutaRateResponse>(request, out var validationError))
            return validationError!;

        await _service.DeleteAsync(request, cancellationTokenSource.Token);

        return Success(new DeleteValutaRateResponse(), "ValutaRate deleted successfully");
    }
}
