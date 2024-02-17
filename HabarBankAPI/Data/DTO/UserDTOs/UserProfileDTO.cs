namespace HabarBankAPI.Data.DTO.UserDTOs
{
    public class UserProfileDTO : UserIdDTO
    {
        public string UserPhone { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserPatronymic { get; set; }
    }
}
