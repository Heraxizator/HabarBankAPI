using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class AccountLevelByIdSpecification : Specification<(UserLevel accountLevel, long accountLevelId)>
    {
        public override Expression<Func<(UserLevel accountLevel, long accountLevelId), bool>> ToExpression()
        {
            return data => data.accountLevel.AccountLevelId == data.accountLevelId && data.accountLevel.Enabled == true;
        }
    }
}
