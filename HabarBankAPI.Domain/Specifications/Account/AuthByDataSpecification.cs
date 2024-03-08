using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class AuthByDataSpecification : Specification<(Account account, string login, string password)>
    {
        public override System.Linq.Expressions.Expression<Func<(Account, string, string), bool>> ToExpression()
        {
            return data => 
                data.Item1.AccountLogin == data.Item2 
                && data.Item1.AccountPassword == data.Item3
                && data.Item1.Enabled == true;
        }
    }
}
