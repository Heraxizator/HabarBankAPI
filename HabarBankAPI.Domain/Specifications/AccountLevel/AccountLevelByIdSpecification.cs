using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities.AccountLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class AccountLevelByIdSpecification : Specification<(AccountLevel, long)>
    {
        public override Expression<Func<(AccountLevel, long), bool>> ToExpression()
        {
            return data => data.Item1.AccountLevelId == data.Item2 && data.Item1.Enabled == true;
        }
    }
}
