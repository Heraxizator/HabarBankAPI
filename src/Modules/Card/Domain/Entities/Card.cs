using Cards.Domain.Enums;
using Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Domain.Entities;

public class Card : BaseEntity
{
    public long UserId { get; set; }
    public long ValutaId { get; set; }
    public long Score { get; set; }
    public required string Number { get; set; }
    public required string Code { get; set; }
    public DateTimeOffset ExpiredAt { get; set; }
    public long TypeId { get; set; }
}
