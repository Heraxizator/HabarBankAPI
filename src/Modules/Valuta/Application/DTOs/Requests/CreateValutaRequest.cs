using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;

namespace Valutas.Application.DTOs.Requests;

public class CreateValutaRequest(ValutaBody body) : BaseRequest
{
    public ValutaBody Body { get; set; } = body;
}
