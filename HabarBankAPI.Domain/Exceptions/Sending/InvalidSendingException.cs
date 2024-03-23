using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Sending
{
    public class InvalidSendingException : Exception
    {
        public InvalidSendingException(string? message) : base(message)
        {
        }
    }
}
