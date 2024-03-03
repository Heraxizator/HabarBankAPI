using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Account
{
    public class BadPasswordException : Exception
    {
        public BadPasswordException(string? message) : base(message)
        {
        }
    }
}
