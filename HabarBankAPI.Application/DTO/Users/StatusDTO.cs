using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Users
{
    public class StatusDTO
    {
        public int UserId { get; set; }
        public bool UserEnabled { get; set; }
        public int UserUserLevelId { get; set; }
    }
}
