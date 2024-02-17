using HabarBankAPI.Data.DTO.UserDTOs;

namespace HabarBankAPI.Data.EntityDTOs.UserDTOs
{
    public class UserStatusDTO : UserIdDTO
    {
        public bool UserEnabled { get; set; }
        public int UserUserLevelId { get; set; }
    }
}
