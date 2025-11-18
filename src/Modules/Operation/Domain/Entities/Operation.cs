using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Domain.Entities;

public class Operation : BaseEntity
{
    public long CardRecipientId { get; set; }
    public long CardSenderId { get; set; }
    public long Score { get; set; }
    public DateTime DateTime { get; set; }
}
