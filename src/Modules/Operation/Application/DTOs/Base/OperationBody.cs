using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Base;

public class OperationBody
{
    [SwaggerSchema(Description = "Идентификатор операции")]
    public long Id { get; set; }

    [Required(ErrorMessage = "CardRecipientId обязателен")]
    [SwaggerSchema(Description = "Идентификатор карты-получателя")]
    public long CardRecipientId { get; set; }

    [Required(ErrorMessage = "CardSenderId обязателен")]
    [SwaggerSchema(Description = "Идентификатор карты-отправителя")]
    public long CardSenderId { get; set; }

    [SwaggerSchema(Description = "Количество средств")]
    [Required(ErrorMessage = "Score обязателен")]
    public long Score { get; set; }

    [SwaggerSchema(Description = "Идентификатор операции")]
    public DateTime DateTime { get; set; }
}
