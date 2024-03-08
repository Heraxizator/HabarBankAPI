using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO
{
    public class AdminDTO
    {
        public long AccountId { get; set; }
        public string AccountLogin { get; set; }
        public string AccountPassword { get; set; }
        public string AccountPhone { get; set; }
        public string AccountName { get; set; }
        public string AccountSurname { get; set; }
        public string AccountPatronymic { get; set; }
    }
}
