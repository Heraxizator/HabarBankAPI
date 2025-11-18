using Card.Application.DTOs.Base;
using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Application.DTOs.Responses;

public class GetCardResponse(CardBody body) : IResponse
{
    public CardBody Body { get; set; } = body;
}
