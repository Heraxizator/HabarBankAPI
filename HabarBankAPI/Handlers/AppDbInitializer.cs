using HabarBankAPI.Data;
using HabarBankAPI.Domain.Entities;

namespace HabarBankAPI.Web.Handlers
{
    public class AppDbInitializer
    {
        private ApplicationDbContext _context { get; set; }
        public AppDbInitializer()
        {
            this._context = new();

            Init();
        }

        public void Init()
        {

            _ = this._context.SaveChanges();
        }

        
    }
}
