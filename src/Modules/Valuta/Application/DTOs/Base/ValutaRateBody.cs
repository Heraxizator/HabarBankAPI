using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valutas.Application.DTOs.Base;

public class ValutaRateBody
{
    public long Id { get; set; }
    public long ValutaId { get; set; }
    public long ValutaCount { get; set; }
    public long RublesCount { get; set; }
}
