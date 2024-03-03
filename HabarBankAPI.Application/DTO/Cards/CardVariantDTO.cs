using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Cards
{
    public class CardVariantDTO
    {
        public int CardVariantId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Percentage { get; set; }
        public int CardTypeId { get; set; }
        public int AccountLevelId { get; set; }
        public bool Enabled { get; set; }
    }
}
