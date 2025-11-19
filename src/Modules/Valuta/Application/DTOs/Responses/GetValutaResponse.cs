using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Application.DTOs.Base;

namespace Valutas.Application.DTOs.Responses;

public class GetValutaResponse(IEnumerable<ValutaBody> objects) : IResponse
{
    public IEnumerable<ValutaBody> Objects { get; set; } = objects;
}
