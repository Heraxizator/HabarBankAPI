using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valutas.Domain.Entities;

public class Valuta : BaseEntity
{
    public required string Name { get; set; }
    public required string DigitalCode { get; set; }
    public required string LetterCode { get; set; }
}
