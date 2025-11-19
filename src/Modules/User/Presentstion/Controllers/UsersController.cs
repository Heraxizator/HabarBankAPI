using Common.Abstracts;
using Common.Constants;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Users.Application.DTOs.Requests;
using Users.Application.DTOs.Responses;
using Users.Application.Interfaces;
using Users.Application.Mappers;
using Users.Domain.Entities;

namespace Standards.Presentation.Controllers;

[ApiController]
[Route("api/v1/users")]

public class UsersController : BaseController
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerResponse(200, "Список пользователей", typeof(IEnumerable<UserBodyWithotPassword>))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserBodyWithotPassword>>>> GetAsync(int perInPage = 20, int page = 1)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        GetUserRequest request = new(perInPage, page);

        if (!ValidateRequest<GetUserRequest, IEnumerable<UserBodyWithotPassword>>(request, out var validationError))
            return validationError!;

        IEnumerable<User> users = await _service.SelectAsync(request, cancellationTokenSource.Token);

        return Success(users.Select(UserMapper.GetBodyWithoutPassword), "Users retrieved successfully");
    }

    [HttpPost]
    [SwaggerResponse(200, "Пользователь успешно создан", typeof(CreateUserResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<CreateUserResponse>>> PostAsync([FromBody] CreateUserRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<CreateUserRequest, CreateUserResponse>(request, out var validationError))
            return validationError!;

        User? user = await _service.CreateAsync(request, cancellationTokenSource.Token);

        if (user is null)
        {
            return NotFound<CreateUserResponse>();
        }

        UserBodyWithotPassword userBody = UserMapper.GetBodyWithoutPassword(user);

        return Success(new CreateUserResponse(
            userBody.Id, userBody.Login, userBody.Email, userBody.FirstName, userBody.MiddleName, userBody.LastName, userBody.RoleId), "User created successfully");
    }

    [HttpPut]
    [SwaggerResponse(200, "Пользователь успешно обновлен", typeof(UpdateUserResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<UpdateUserResponse>>> PutAsync([FromBody] UpdateUserRequest request)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        if (!ValidateRequest<UpdateUserRequest, UpdateUserResponse>(request, out var validationError))
            return validationError!;

        await _service.UpdateAsync(request, cancellationTokenSource.Token);

        return Success(new UpdateUserResponse(), "User updated successfully");
    }

    [HttpDelete]
    [SwaggerResponse(200, "Пользователь успешно удален", typeof(DeleteUserResponse))]
    [SwaggerResponse(400, "Ошибка валидации")]
    [SwaggerResponse(404, "Ресурс не найден")]
    public async Task<ActionResult<ApiResponse<DeleteUserResponse>>> DeleteAsync(long id)
    {
        using CancellationTokenSource cancellationTokenSource = new(Constants.BaseTimeout);

        DeleteUserRequest request = new(id);

        if (!ValidateRequest<DeleteUserRequest, DeleteUserResponse>(request, out var validationError))
            return validationError!;

        await _service.DeleteAsync(request, cancellationTokenSource.Token);

        return Success(new DeleteUserResponse(), "User deleted successfully");
    }
}
