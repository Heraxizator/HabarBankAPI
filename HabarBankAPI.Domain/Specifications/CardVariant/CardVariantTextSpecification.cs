using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.CardVariant
{
    public class CardVariantTextSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return text => !string.IsNullOrEmpty(text) && text.Length >= 15;
        }
    }
}
