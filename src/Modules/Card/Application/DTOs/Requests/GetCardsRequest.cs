using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.DTOs.Requests;

public class GetCardsRequest(long userId) : BaseRequest
{
    public long UserId { get; set; } = userId;
}
