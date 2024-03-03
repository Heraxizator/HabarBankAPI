using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.Card
{
    public class CardVariantNotFoundException : Exception
    {
        public CardVariantNotFoundException(string? message) : base(message)
        {
        }
    }
}
