
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Database;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Card;
using HabarBankAPI.Domain.Entities.MetalBill;
using HabarBankAPI.Domain.Entities.MetalScore;
using HabarBankAPI.Domain.Entities.MetalScore.MetalScore;
using HabarBankAPI.Domain.Entities.Operation;
using HabarBankAPI.Domain.Entities.Substance;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Entities.ValutaBill;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Action = HabarBankAPI.Domain.Entities.Operation.Action;

namespace HabarBankAPI.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        #region Tables
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<CardVariant> CardVariants { get; set; }
        public DbSet<Substance> Entities { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Metal> Metals { get; set; }
        public DbSet<MetalScore> MetalStorages { get; set; }
        public DbSet<Action> Operations { get; set; }
        public DbSet<ActionType> OperationTypes { get; set; }
        public DbSet<Sending> Transfers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AccountLevel> UserLevels { get; set; }
        public DbSet<Valuta> Valutas { get; set; }
        public DbSet<ValutaScore> ValutaStorages { get; set; }
        public DbSet<ValutaScoreVariant> ValutaStorageVariants { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _ = options.UseSqlite($"Data Source=db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
