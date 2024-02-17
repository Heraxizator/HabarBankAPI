using HabarBankAPI.Data.Entities;

namespace HabarBankAPI.Data
{
    public class AppDbInitializer
    {
        private AppDbContext _context { get; set; }
        public AppDbInitializer()
        {
            this._context = new AppDbContext();

            Init();
        }

        public void Init()
        {
            SetUserLevels();

            _ = this._context.SaveChanges();
        }

        private void SetUserLevels()
        {
            if (this._context.Set<UserLevel>().Any())
            {
                return;
            }

            this._context.AddRange
            (
                new UserLevel
                {
                    UserLevelName = "Стандартный",
                    UserLevelEnabled = true,
                },

                new UserLevel
                {
                    UserLevelName = "Улучшенный",
                    UserLevelEnabled = true,
                }
            );
        }
    }
}
