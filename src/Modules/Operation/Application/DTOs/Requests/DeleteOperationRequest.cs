using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Requests;

public class DeleteOperationRequest(long id) : BaseRequest
{
    public long Id { get; set; } = id;
}
