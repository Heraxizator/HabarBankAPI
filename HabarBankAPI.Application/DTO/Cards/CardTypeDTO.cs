using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Cards
{
    public class CardTypeDTO
    {
        public int CardTypeId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
