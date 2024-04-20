using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Cards
{
    public class CardDTO
    {
        public long CardId { get; set; }
        public string? CardNumber { get; set; }
        public long CardVariantId { get; set; }
        public long UserId { get; set; }
        public int RublesCount { get; set; }
        public string? ImagePath { get; set; }
        public bool Enabled { get; set; }
    }
}
