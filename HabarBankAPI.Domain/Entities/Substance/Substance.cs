using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public abstract class Substance : Entity, IAggregateRoot
    {
        public Substance() { }

        public int RublesCount { get; set; }
        public User User { get; protected set; }

        public ICollection<Operation> Operations { get; }
        public ICollection<Sending> Transfers { get; }
    }
}
