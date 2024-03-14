namespace HabarBankAPI.Application.DTO.Admins
{
    public class AdminDTO
    {
        public long AdminId { get; set; }
        public required string AccountLogin { get; set; }
        public required string AccountPassword { get; set; }
        public required string AccountPhone { get; set; }
        public required string AccountName { get; set; }
        public required string AccountSurname { get; set; }
        public required string AccountPatronymic { get; set; }
        public bool Enabled { get; set; }
    }
}
