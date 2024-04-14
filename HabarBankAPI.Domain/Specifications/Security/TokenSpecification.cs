using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Security
{
    public class TokenSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return token => !string.IsNullOrEmpty(token) && token.Length >= 5;
        }
    }
}
