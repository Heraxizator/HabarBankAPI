using Card.Application.DTOs.Base;
using Card.Domain.Enums;
using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Application.DTOs.Requests;

public class UpdateCardRequest(long id, long userId, long rublesCount, string number, string code, DateTimeOffset expiredAt, CardTypes typeId) : BaseRequest
{
    public long Id { get; set; } = id;
    public long UserId { get; set; } = userId;
    public long RublesCount { get; set; } = rublesCount;
    public required string Number { get; set; } = number;
    public required string Code { get; set; } = code;
    public DateTimeOffset ExpiredAt { get; set; } = expiredAt;
    public CardTypes TypeId { get; set; } = typeId;
}
