using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.AccountLevel
{
    public class BadNameException : Exception
    {
        public BadNameException(string? message) : base(message)
        {
        }
    }
}
