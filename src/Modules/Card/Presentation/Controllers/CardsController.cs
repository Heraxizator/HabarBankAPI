using Card.Application.DTOs.Base;
using Card.Application.DTOs.Requests;
using Card.Application.DTOs.Responses;
using Card.Application.Interfaces;
using Card.Application.Mappers;
using Common.Abstracts;
using Common.Constants;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Standards.Presentation.Controllers;

[ApiController]
[Route("api/v1/cards")]

public class CardsController : BaseController
{
    private readonly ICardService _service;
    public CardsController(ICardService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerResponse(200, "Список карт", typeof(CardBody))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<ActionResult<ApiResponse<CardBody>>> GetAsync(long id)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        GetCardRequest request = new(id);

        if (!ValidateRequest<GetCardRequest, CardBody>(request, out var validationError))
            return validationError!;

        Card.Domain.Entities.Card? card = await _service.GetAsync(request, cancellationTokenSource.Token);

        if (card is null)
        {
            return NotFound<CardBody>();
        }

        return Success(CardMapper.GetBody(card), "Cards retrieved successfully");
    }

    [HttpPost]
    [SwaggerResponse(200, "Карта успешно создан", typeof(CreateCardResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<CreateCardResponse>>> PostAsync([FromBody] CreateCardRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<CreateCardRequest, CreateCardResponse>(request, out var validationError))
            return validationError!;

        Card.Domain.Entities.Card? card = await _service.CreateAsync(request, cancellationTokenSource.Token);

        if (card is null)
        {
            return NotFound<CreateCardResponse>();
        }

        return Success(new CreateCardResponse(CardMapper.GetBody(card)), "Card created successfully");
    }

    [HttpPut]
    [SwaggerResponse(200, "Карта успешно обновлена", typeof(UpdateCardResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<UpdateCardResponse>>> PutAsync([FromBody] UpdateCardRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<UpdateCardRequest, UpdateCardResponse>(request, out var validationError))
            return validationError!;

        await _service.UpdateAsync(request, cancellationTokenSource.Token);

        return Success(new UpdateCardResponse(), "Card updated successfully");
    }

    [HttpDelete]
    [SwaggerResponse(200, "Карта успешно удалена", typeof(DeleteCardResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<DeleteCardResponse>>> DeleteAsync(long id)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        DeleteCardRequest request = new(id);

        if (!ValidateRequest<DeleteCardRequest, DeleteCardResponse>(request, out var validationError))
            return validationError!;

        await _service.DeleteAsync(request, cancellationTokenSource.Token);

        return Success(new DeleteCardResponse(), "Card deleted successfully");
    }
}
