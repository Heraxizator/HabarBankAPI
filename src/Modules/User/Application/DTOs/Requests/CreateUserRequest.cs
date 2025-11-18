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

[SwaggerSchema(Title = "CreateUserRequest", Description = "Запрос для создания нового пользователя")]
public class CreateUserRequest(UserBodyWithPassword body) : BaseRequest
{
    [Required(ErrorMessage = "Body обязателен")]
    [SwaggerSchema(Description = "Данные пользователя для создания")]
    public UserBodyWithPassword Body { get; set; } = body;
}
