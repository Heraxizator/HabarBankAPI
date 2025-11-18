using Operations.Application.DTOs.Base;
using Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.Mappers;

public static class OperationMapper
{
    public static Operation GetModel(OperationBody body)
    {
        return new Operation
        {
            Id = body.Id,
            CardSenderId = body.CardSenderId,
            CardRecipientId = body.CardRecipientId,
            Score = body.Score,
            DateTime = body.DateTime,
        };
    }

    public static OperationBody GetBody(Operation model)
    {
        return new OperationBody
        {
            Id = model.Id,
            CardSenderId = model.CardSenderId,
            CardRecipientId = model.CardRecipientId,
            Score = model.Score,
            DateTime = model.DateTime,
        };
    }
}
