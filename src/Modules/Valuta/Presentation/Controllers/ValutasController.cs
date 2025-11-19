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
[Route("api/v1/valutas")]

public class ValutasController : BaseController
{
    private readonly IValutaService _service;
    public ValutasController(IValutaService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerResponse(200, "Список валют", typeof(IEnumerable<ValutaBody>))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<ActionResult<ApiResponse<IEnumerable<ValutaBody>>>> GetAsync(int perInPage = 20, int page = 1)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        GetValutaRequest request = new(perInPage, page);

        if (!ValidateRequest<GetValutaRequest, IEnumerable<ValutaBody>>(request, out var validationError))
            return validationError!;

        IEnumerable<Valuta> Valutas = await _service.GetAsync(request, cancellationTokenSource.Token);

        return Success(Valutas.Select(ValutaMapper.GetBody), "Valutas retrieved successfully");
    }

    [HttpPost]
    [SwaggerResponse(200, "Валюта успешно создана", typeof(CreateValutaResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<CreateValutaResponse>>> PostAsync([FromBody] CreateValutaRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<CreateValutaRequest, CreateValutaResponse>(request, out var validationError))
            return validationError!;

        Valuta? Valuta = await _service.CreateAsync(request, cancellationTokenSource.Token);

        if (Valuta is null)
        {
            return NotFound<CreateValutaResponse>();
        }

        ValutaBody valutaBody = ValutaMapper.GetBody(Valuta);

        return Success(new CreateValutaResponse(valutaBody), "Valuta created successfully");
    }

    [HttpPut]
    [SwaggerResponse(200, "Валюта успешно обновлена", typeof(UpdateValutaResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<UpdateValutaResponse>>> PutAsync([FromBody] UpdateValutaRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<UpdateValutaRequest, UpdateValutaResponse>(request, out var validationError))
            return validationError!;

        await _service.UpdateAsync(request, cancellationTokenSource.Token);

        return Success(new UpdateValutaResponse(), "Valuta updated successfully");
    }

    [HttpDelete]
    [SwaggerResponse(200, "Валюта успешно удалена", typeof(DeleteValutaResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<DeleteValutaResponse>>> DeleteAsync(long id)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        DeleteValutaRequest request = new(id);

        if (!ValidateRequest<DeleteValutaRequest, DeleteValutaResponse>(request, out var validationError))
            return validationError!;

        await _service.DeleteAsync(request, cancellationTokenSource.Token);

        return Success(new DeleteValutaResponse(), "Valuta deleted successfully");
    }
}

