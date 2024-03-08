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
    public class AccountsListSpecification : Specification<Account>
    {
        public override Expression<Func<Account, bool>> ToExpression()
        {
            return account => account.Enabled == true;
        }
    }
}
