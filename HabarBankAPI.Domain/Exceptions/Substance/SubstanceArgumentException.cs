using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Substance
{
    public class SubstanceArgumentException : Exception
    {
        public SubstanceArgumentException(string? message) : base(message)
        {
        }
    }
}
