using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Security
{
    public class EmailSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return email => email.Length >= 5
                   && email.Contains("@")
                   && email.Contains(".");
        }
    }
}
