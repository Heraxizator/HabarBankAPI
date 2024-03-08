using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Card
{
    public class RublesCountArgumentException : Exception
    {
        public RublesCountArgumentException(string? message) : base(message)
        {
        }
    }
}
