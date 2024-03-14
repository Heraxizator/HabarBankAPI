using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Cards
{
    public class CardVariantDTO
    {
        public long CardVariantId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Percentage { get; set; }
        public long CardTypeId { get; set; }
        public long UserLevelId { get; set; }
        public bool Enabled { get; set; }
    }
}
