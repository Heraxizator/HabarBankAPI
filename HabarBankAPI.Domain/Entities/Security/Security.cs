using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.Security
{
    public class Security : Entity, IAggregateRoot
    {
        public Security() { }

        public Security(string? email, string? token)
        {
            this.Email = email;
            this.Token = token;
        }

        public void SetToken()
        {
            this.Token = Guid.NewGuid().ToString();
        }

        [Key]
        public long Id { get; private init; }
        public string? Email { get; private set; }
        public string? Token { get; private set; }
    }
}
