using Common.Abstracts;
using Common.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.DTOs.Requests;

[SwaggerSchema(Title = "UpdateUserRequest", Description = "Запрос для обновления существующего пользователя")]
public class UpdateUserRequest(UserBodyWithotPassword body) : BaseRequest
{
    [Required(ErrorMessage = "Body обязателен")]
    [SwaggerSchema(Description = "Обновленные данные стандарта")]
    public UserBodyWithotPassword Body { get; set; } = body;
}
