using Card.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Application.Mappers;

public static class CardMapper
{
    public static Domain.Entities.Card GetModel(CardBody body)
    {
        return new Domain.Entities.Card
        {
            Id = body.Id,
            Number = body.Number,
            Code = body.Code,
            Score = body.RublesCount,
            UserId = body.UserId,
            TypeId = body.TypeId,
            ExpiredAt = body.ExpiredAt
        };
    }

    public static CardBody GetBody(Domain.Entities.Card model)
    {
        return new CardBody
        {
            Id = model.Id,
            Number = model.Number,
            Code = model.Code,
            RublesCount = model.Score,
            UserId = model.UserId,
            TypeId = model.TypeId,
            ExpiredAt = model.ExpiredAt
        };
    }
}
