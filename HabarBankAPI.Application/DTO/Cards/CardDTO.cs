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
        public int SubstanceId { get; set; }
        public int CardVariantId { get; set; }
        public int AccountId { get; set; }
        public int RublesCount { get; set; }
        public string ImagePath { get; set; }
        public bool Enabled { get; set; }
    }
}
