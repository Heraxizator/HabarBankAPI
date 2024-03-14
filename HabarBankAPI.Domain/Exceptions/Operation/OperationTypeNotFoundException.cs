using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Action
{
    public class OperationTypeNotFoundException : Exception
    {
        public OperationTypeNotFoundException(string? message) : base(message)
        {
        }
    }
}
