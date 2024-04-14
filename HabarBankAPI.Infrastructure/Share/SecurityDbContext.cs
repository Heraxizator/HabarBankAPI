using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Entities.ValutaBill;
using HabarBankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabarBankAPI.Domain.Entities.Security;

namespace HabarBankAPI.Infrastructure.Share
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        #region Tables
        public DbSet<Security> Consumers { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _ = options.UseSqlite($"Data Source=security");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Security>()
                .HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
