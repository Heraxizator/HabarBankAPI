using Common.Abstracts;
using Operations.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Responses;

public class GetOperationsResponse(IEnumerable<OperationBody> objects) : IResponse
{
    public IEnumerable<OperationBody> Objects { get; set; } = objects;
}
