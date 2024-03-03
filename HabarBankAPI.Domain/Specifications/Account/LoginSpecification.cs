using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Account
{
    public class LoginSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return login => !string.IsNullOrEmpty(login)
                && login.Count(c => char.IsLetter(c)) > 0
                && login.Length >= 5;
        }
    }
}
