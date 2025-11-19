using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;
using Valutas.Domain.Entities;

namespace Valutas.Application.DTOs.Responses;

public class GetValutaRateResponse(IEnumerable<ValutaRateBody> objects) : IResponse
{
    public IEnumerable<ValutaRateBody> Objects { get; set; } = objects;
}
