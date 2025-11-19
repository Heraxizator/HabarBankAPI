using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;
using Valutas.Domain.Entities;

namespace Valutas.Application.Mappers;

public static class ValutaMapper
{
    public static Valuta GetModel(ValutaBody body)
    {
        return new Valuta 
        { 
            Id = body.Id,
            DigitalCode = body.DigitalCode,
            LetterCode = body.LetterCode,
            Name = body.Name,
        };
    }

    public static ValutaBody GetBody(Valuta model)
    {
        return new ValutaBody
        {
            Id = model.Id,
            DigitalCode = model.DigitalCode,
            LetterCode = model.LetterCode,
            Name = model.Name,
        };
    }
}
