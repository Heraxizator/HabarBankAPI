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
    public class AccountByIdSpecification : Specification<(Account, long)>
    {
        public override Expression<Func<(Account, long), bool>> ToExpression()
        {
            return data => data.Item1.AccountId == data.Item2 && data.Item1.Enabled == true;
        }
    }
}
