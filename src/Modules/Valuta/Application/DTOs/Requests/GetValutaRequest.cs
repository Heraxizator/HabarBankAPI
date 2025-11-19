using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valutas.Application.DTOs.Requests;

public class GetValutaRequest(int page, int perInPage) : BaseRequest
{
    public int Page { get; set; } = page;
    public int PerInPage { get; set; } = perInPage;
}
