using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("entity")]
    public class Entity
    {
        [Column("entity_id"), Key]
        public int EntityId { get; set; }

        [Column("entity_user_id")]
        public int EntityUserId { get; set; }

        [Column("entity_enabled")]
        public bool EntityEnabled { get; set; }

        public User User { get; set; }
        public ICollection<Operation> Operations { get; set; }
    }
}
