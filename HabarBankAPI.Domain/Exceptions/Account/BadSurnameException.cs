using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Account
{
    public class BadSurnameException : Exception
    {
        public BadSurnameException(string? message) : base(message)
        {
        }
    }
}
