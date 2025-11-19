using Cards.Application.DTOs.Base;
using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Application.DTOs.Responses;

public class GetCardsResponse(IEnumerable<CardBody> objects) : IResponse
{
    public IEnumerable<CardBody> Objects { get; set; } = objects;
}
