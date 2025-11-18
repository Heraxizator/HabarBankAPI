using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.DTOs.Base;

public class OperationBody
{
    public long Id { get; set; }
    public long CardRecipientId { get; set; }
    public long CardSenderId { get; set; }
    public long Score { get; set; }
    public DateTime DateTime { get; set; }
}
