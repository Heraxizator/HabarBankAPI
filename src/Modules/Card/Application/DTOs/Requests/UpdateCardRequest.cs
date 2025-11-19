using Cards.Application.DTOs.Base;
using Cards.Domain.Enums;
using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.DTOs.Requests;

public class UpdateCardRequest(CardBody body) : BaseRequest
{
    public CardBody Body { get; set; } = body;
}
