using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;
using Valutas.Domain.Entities;

namespace Valutas.Application.Mappers;

public static class ValutaRateMapper
{
    public static ValutaRate GetModel(ValutaRateBody body)
    {
        return new ValutaRate
        {
            Id = body.Id,
            ValutaId = body.ValutaId,
            ValutaCount = body.ValutaCount,
            RublesCount = body.RublesCount,
        };
    }

    public static ValutaRateBody GetBody(ValutaRate model)
    {
        return new ValutaRateBody
        {
            Id = model.Id,
            ValutaId = model.ValutaId,
            ValutaCount = model.ValutaCount,
            RublesCount = model.RublesCount,
        };
    }
}
