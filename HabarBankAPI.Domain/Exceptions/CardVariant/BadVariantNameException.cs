using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.CardVariant
{
    public class BadVariantNameException : Exception
    {
        public BadVariantNameException(string? message) : base(message)
        {
        }
    }
}
