using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("userlevel")]
    public class UserLevel
    {
        [Column("userlevel_id"), Key]
        public int UserLevelId { get; set; }

        [Column("userlevel_name")]
        public string UserLevelName { get; set; }

        [Column("userlevel_enabled")]
        public bool UserLevelEnabled { get; set; }

        public ICollection<ValutaStorageVariant> valutaStorageVariants { get; set; }
        public ICollection<CardVariant> CardVariants { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
