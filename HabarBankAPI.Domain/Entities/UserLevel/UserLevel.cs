using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities
{
    public class UserLevel : Entity, IAggregateRoot
    {
        public UserLevel() { }
        public UserLevel(string name)
        {
            this.Name = name;
            this.Enabled = true;
        }

        [Key]
        public long AccountLevelId { get; private set; }
        public string? Name { get; set; }
        public ICollection<User> Users { get; }
        public ICollection<CardVariant> CardVariants { get; }

        public void SetAccountLevelName(string name)
        {
            this.Name = name;
        }
    }
}
