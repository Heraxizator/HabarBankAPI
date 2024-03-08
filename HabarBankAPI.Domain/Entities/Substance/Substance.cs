using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.Substance
{
    public abstract class Substance : Entity, IAggregateRoot
    {
        [Key]
        public long SubstanceId { get; protected set; }
        public long AccountId { get; private init; }
        public int RublesCount { get; set; }
    }
}
