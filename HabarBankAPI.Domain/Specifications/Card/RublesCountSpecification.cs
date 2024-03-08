using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Card
{
    public class RublesCountSpecification : Specification<int>
    {
        public override Expression<Func<int, bool>> ToExpression()
        {
            return count => count > 0;
        }
    }
}
