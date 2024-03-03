using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Account
{
    public class PhoneSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return phone => phone.Length == 12
                && phone.FirstOrDefault() == '+'
                && phone.Count(c => c == '+') == 1
                && phone.Count(c => c == '(' || c == ')') == 0
                && phone.Count(c => "1234567890".Contains(c)) == 11;
        }
    }
}
