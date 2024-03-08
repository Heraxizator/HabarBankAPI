using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Users
{
    public class StatusDTO
    {
        public long UserId { get; set; }
        public bool UserEnabled { get; set; }
        public long UserLevelId { get; set; }
    }
}
