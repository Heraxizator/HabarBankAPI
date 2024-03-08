using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Account
{
    public class AccountAlreadyExistsException : Exception
    {
        public AccountAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
