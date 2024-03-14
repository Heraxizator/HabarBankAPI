using HabarBankAPI.Domain.Abstractions;
using HabarBankAPI.Domain.Entities;
using System.Linq.Expressions;

namespace HabarBankAPI.Domain
{
    public class OperationByIdSpecification : Specification<(Operation operation, long operationId)>
    {
        public override Expression<Func<(Operation operation, long operationId), bool>> ToExpression()
        {
            return data => data.operation.OperationId == data.operationId && data.operation.Enabled == true;
        }
    }
}
