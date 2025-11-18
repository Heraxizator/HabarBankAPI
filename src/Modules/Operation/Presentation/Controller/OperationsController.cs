using Common.Abstracts;
using Common.Constants;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.DTOs.Base;
using Operations.Application.DTOs.Requests;
using Operations.Application.DTOs.Responses;
using Operations.Application.Interfaces;
using Operations.Application.Mappers;
using Operations.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Standards.Presentation.Controllers;

[ApiController]
[Route("api/v1/operations")]

public class OperationsController : BaseController
{
    private readonly IOperationService _service;
    public OperationsController(IOperationService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerResponse(200, "Список операций", typeof(IEnumerable<OperationBody>))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<ActionResult<ApiResponse<IEnumerable<OperationBody>>>> GetAsync(long cardId, int perInPage = 20, int page = 1)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        GetOperationsRequest request = new(cardId, perInPage, page);

        if (!ValidateRequest<GetOperationsRequest, IEnumerable<OperationBody>>(request, out var validationError))
            return validationError!;

        IEnumerable<Operation> operations = await _service.GetAsync(request, cancellationTokenSource.Token);

        return Success(operations.Select(OperationMapper.GetBody), "Operations retrieved successfully");
    }

    [HttpPost]
    [SwaggerResponse(200, "Операция успешно создана", typeof(CreateOperationResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<CreateOperationResponse>>> PostAsync([FromBody] CreateOperationRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<CreateOperationRequest, CreateOperationResponse>(request, out var validationError))
            return validationError!;

        Operation operation = await _service.CreateAsync(request, cancellationTokenSource.Token);

        return Success(new CreateOperationResponse(OperationMapper.GetBody(operation)), "Operation created successfully");
    }

    [HttpPut]
    [SwaggerResponse(200, "Операция успешно обновлена", typeof(UpdateOperationResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<UpdateOperationResponse>>> PutAsync([FromBody] UpdateOperationRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<UpdateOperationRequest, UpdateOperationResponse>(request, out var validationError))
            return validationError!;

        await _service.UpdateAsync(request, cancellationTokenSource.Token);

        return Success(new UpdateOperationResponse(), "Operation updated successfully");
    }

    [HttpDelete]
    [SwaggerResponse(200, "Операция успешно удалена", typeof(DeleteOperationResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<DeleteOperationResponse>>> DeleteAsync(long id)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        DeleteOperationRequest request = new(id);

        if (!ValidateRequest<DeleteOperationRequest, DeleteOperationResponse>(request, out var validationError))
            return validationError!;

        await _service.DeleteAsync(request, cancellationTokenSource.Token);

        return Success(new DeleteOperationResponse(), "Operation deleted successfully");
    }
}
