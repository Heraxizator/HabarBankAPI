using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Sending
{
    public class SendingNotFoundException : Exception
    {
        public SendingNotFoundException(string? message) : base(message)
        {
        }
    }
}
