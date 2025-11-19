using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valutas.Domain.Entities;

public class ValutaRate : BaseEntity
{
    public long ValutaId { get; set; }
    public long ValutaCount { get; set; }
    public long RublesCount { get; set; }
}
