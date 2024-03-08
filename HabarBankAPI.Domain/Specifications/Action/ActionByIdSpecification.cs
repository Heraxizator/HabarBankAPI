using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Action = HabarBankAPI.Domain.Entities.Operation.Action;

namespace HabarBankAPI.Domain
{
    public class ActionByIdSpecification : Specification<(Action, long)>
    {
        public override Expression<Func<(Action, long), bool>> ToExpression()
        {
            return data => data.Item1.ActionId == data.Item2 && data.Item1.Enabled == true;
        }
    }
}
