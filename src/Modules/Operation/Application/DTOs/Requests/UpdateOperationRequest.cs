using Common.Abstracts;
using Operations.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Requests;

public class UpdateOperationRequest(OperationBody body) : BaseRequest
{
    public OperationBody Body { get; set; } = body;
}
