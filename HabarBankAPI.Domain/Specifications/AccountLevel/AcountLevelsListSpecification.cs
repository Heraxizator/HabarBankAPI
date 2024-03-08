using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities.AccountLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class AcountLevelsListSpecification : Specification<AccountLevel>
    {
        public override Expression<Func<AccountLevel, bool>> ToExpression()
        {
            return level => level.Enabled == true;
        }
    }
}
