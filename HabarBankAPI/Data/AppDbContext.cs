using HabarBankAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabarBankAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            this.Database.EnsureCreated();
        }

        #region Tables
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<CardVariant> CardVariants { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Metal> Metals { get; set; }
        public DbSet<MetalStorage> MetalStorages { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<Valuta> Valutas { get; set; }
        public DbSet<ValutaStorage> ValutaStorages { get; set; }
        public DbSet<ValutaStorageVariant> valutaStorageVariants { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _ = options.UseSqlite($"Data Source=db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Operation>()
            .HasOne(a => a.Transfer)
            .WithOne(a => a.Operation)
            .HasForeignKey<Transfer>(c => c.OperationId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
