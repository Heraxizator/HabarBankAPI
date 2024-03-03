using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Share
{
    public abstract class Entity
    {
        public Entity() { }

        public bool Enabled { get; protected set; }

        public void SetEnabled(bool enabled) 
        {
            this.Enabled = enabled;
        }
    }
}
