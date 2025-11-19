using Cards.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.DTOs.Base;

[SwaggerSchema(Description = "Данные карты")]
public class CardBody
{
    [SwaggerSchema(Description = "Идентификатор стандарта")]
    public long Id { get; set; }

    [Required(ErrorMessage = "UserId обязателен")]
    [SwaggerSchema(Description = "Идентификатор пользователя карты")]
    public long UserId { get; set; }

    [Required(ErrorMessage = "ValutaId обязателен")]
    [SwaggerSchema(Description = "Идентификатор валюты карты")]
    public long ValutaId { get; set; }

    [SwaggerSchema(Description = "Количество средств на карте")]
    public long RublesCount { get; set; }

    [Required(ErrorMessage = "Number обязателен")]
    [SwaggerSchema(Description = "Номер карты")]
    public required string Number { get; set; }

    [Required(ErrorMessage = "Code обязателен")]
    [SwaggerSchema(Description = "Код карты")]
    public required string Code { get; set; }

    [SwaggerSchema(Description = "Срок карты")]
    public DateTimeOffset ExpiredAt { get; set; }

    [Required(ErrorMessage = "TypeId обязателен")]
    [SwaggerSchema(Description = "Идентификатор типа карты")]
    public long TypeId { get; set; }
}
