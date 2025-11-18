using Common.Abstracts;
using Operations.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Requests;

public class GetOperationsRequest(long cardId, int page, int perInPage) : BaseRequest
{
    public long CardId { get; set; } = cardId;
    public int Page { get; set; } = page;
    public int PerInPage { get; set; } = perInPage;
}
