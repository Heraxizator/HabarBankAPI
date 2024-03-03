using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Account
{
    public class NameSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return name => !string.IsNullOrEmpty(name)
                && name.Count(c => char.IsLetter(c)) == name.Length
                && char.IsUpper(name.FirstOrDefault());
        }
    }
}
