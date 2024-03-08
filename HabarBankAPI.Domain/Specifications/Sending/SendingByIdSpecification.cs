using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class SendingByIdSpecification : Specification<(Sending, long)>
    {
        public override Expression<Func<(Sending, long), bool>> ToExpression()
        {
            return data => data.Item1.ActionId == data.Item2 && data.Item1.Enabled == true;
        }
    }
}
