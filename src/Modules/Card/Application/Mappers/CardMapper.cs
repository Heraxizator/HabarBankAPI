using Cards.Application.DTOs.Base;
using Cards.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.Mappers;

public static class CardMapper
{
    public static Card GetModel(CardBody body)
    {
        return new Card
        {
            Id = body.Id,
            ValutaId = body.ValutaId,
            Number = body.Number,
            Code = body.Code,
            Score = body.RublesCount,
            UserId = body.UserId,
            TypeId = body.TypeId,
            ExpiredAt = body.ExpiredAt
        };
    }

    public static CardBody GetBody(Card model)
    {
        return new CardBody
        {
            Id = model.Id,
            ValutaId = model.ValutaId,
            Number = model.Number,
            Code = model.Code,
            RublesCount = model.Score,
            UserId = model.UserId,
            TypeId = model.TypeId,
            ExpiredAt = model.ExpiredAt
        };
    }
}
