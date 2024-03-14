using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Action
{
    public class OperationNotFoundException : Exception
    {
        public OperationNotFoundException(string? message) : base(message)
        {
        }
    }
}
