using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Account
{
    public class SurnameSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return surname => !string.IsNullOrEmpty(surname)
                && surname.Count(c => char.IsLetter(c)) == surname.Length
                && char.IsUpper(surname.FirstOrDefault());
        }
    }
}
