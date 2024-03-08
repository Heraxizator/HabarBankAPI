using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities.Substance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class SubstanceByIdSpecification : Specification<(Substance, long)>
    {
        public override Expression<Func<(Substance, long), bool>> ToExpression()
        {
            return data => data.Item1.AccountId == data.Item2 && data.Item1.Enabled == true;
        }
    }
}
