using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("user")]
    public class User
    {
        [Column("user_id"), Key]
        public int UserId { get; set; }

        [Column("user_phone")]
        public string UserPhone { get; set; }

        [Column("user_login")]
        public string UserLogin { get; set; }

        [Column("user_password")]
        public string UserPassword { get; set; }

        [Column("user_name")]
        public  string UserName { get; set; }

        [Column("user_surname")]
        public  string UserSurname { get; set; }

        [Column("user_patronymic")]
        public  string UserPatronymic { get; set; }

        [Column("user_userlevel_id")]
        public  int UserUserLevelId { get; set; }

        [Column("user_enabled")]
        public bool UserEnabled { get; set; }

        public ICollection<Entity> Entities { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}
