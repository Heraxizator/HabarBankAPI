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
    public class AcountLevelsListSpecification : Specification<UserLevel>
    {
        public override Expression<Func<UserLevel, bool>> ToExpression()
        {
            return level => level.Enabled == true;
        }
    }
}
