using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;

namespace Valutas.Application.DTOs.Requests;

public class UpdateValutaRateRequest(ValutaRateBody body) : BaseRequest
{
    public ValutaRateBody Body { get; set; } = body;
}
