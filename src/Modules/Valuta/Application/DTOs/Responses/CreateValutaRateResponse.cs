using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;

namespace Valutas.Application.DTOs.Responses;

public class CreateValutaRateResponse(ValutaRateBody body) : IResponse
{
    public ValutaRateBody Body { get; set; } = body;
}
