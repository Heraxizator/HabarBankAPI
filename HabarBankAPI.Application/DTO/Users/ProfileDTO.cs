using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Users
{
    public class ProfileDTO
    {
        public int UserId { get; set; }
        public string UserPhone { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
    }
}
