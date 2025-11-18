using Common.Abstracts;
using Operations.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Requests;

public class CreateOperationRequest(long id, long cardRecipientId, long cardSenderId, long score, DateTime dateTime) : BaseRequest
{
    public long Id { get; set; } = id;
    public long CardRecipientId { get; set; } = cardRecipientId;
    public long CardSenderId { get; set; } = cardSenderId;
    public long Score { get; set; } = score;
    public DateTime DateTime { get; set; } = dateTime;
}
