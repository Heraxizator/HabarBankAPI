using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Substance
{
    public class SubstanceNotFoundException : Exception
    {
        public SubstanceNotFoundException(string? message) : base(message)
        {
        }
    }
}
