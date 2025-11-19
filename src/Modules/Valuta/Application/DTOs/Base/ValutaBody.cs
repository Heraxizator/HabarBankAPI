using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valutas.Application.DTOs.Base;

public class ValutaBody
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string DigitalCode { get; set; }
    public required string LetterCode { get; set; }
}
