using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Account
{
    public class BadPhoneException : Exception
    {
        public BadPhoneException(string? message) : base(message)
        {
        }
    }
}
