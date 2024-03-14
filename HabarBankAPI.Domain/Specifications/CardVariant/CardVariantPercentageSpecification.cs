using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.CardVariant
{
    public class CardVariantPercentageSpecification : Specification<int>
    {
        public override Expression<Func<int, bool>> ToExpression()
        {
            return percentage => percentage >= 0;
        }
    }
}
