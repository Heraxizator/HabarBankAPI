using HabarBankAPI.Domain.Abstractions.Database;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Entities.ValutaBill;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace HabarBankAPI.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Metal> Metals { get; set; }
        public DbSet<MetalScore> MetalStorages { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Sending> Transfers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
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
            modelBuilder.Entity<Card>()
                .HasMany(e => e.Transfers)
                .WithOne(e => e.Card);

            base.OnModelCreating(modelBuilder);
        }
    }
}
