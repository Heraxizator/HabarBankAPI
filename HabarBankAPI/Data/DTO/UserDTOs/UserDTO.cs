using HabarBankAPI.Data.DTO.UserDTOs;

namespace HabarBankAPI.Data.DTO
{
    public class UserDTO : UserIdDTO
    {
        public string UserPhone { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
        public bool UserEnabled { get; set; }
        public int UserUserLevelId { get; set; }
    }
}
