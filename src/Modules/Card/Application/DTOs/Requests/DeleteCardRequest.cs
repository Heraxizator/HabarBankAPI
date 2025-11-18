using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Application.DTOs.Requests;

public class DeleteCardRequest(long id) : BaseRequest
{
    public long Id { get; set; } = id;
}
