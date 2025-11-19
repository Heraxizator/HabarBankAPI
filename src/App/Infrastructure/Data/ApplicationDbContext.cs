using Microsoft.EntityFrameworkCore;
using Common.Abstracts;
using Users.Domain.Entities;
using Access.Domain.Entities;
using Operations.Domain.Entities;
using Valutas.Domain.Entities;

namespace App.Infrastructure.Data;

#pragma warning disable 1591
public class ApplicationDbContext : DbContext, IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Valuta> Valutas { get; set; } = default!;
    public DbSet<ValutaRate> ValutaRates { get; set; } = default!;
    public DbSet<Operation> Operations { get; set; } = default!;
    public DbSet<Cards.Domain.Entities.Card> Cards { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<AccessToken> AccessTokens { get; set; } = default!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка сущности ValutaRate
        modelBuilder.Entity<ValutaRate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("valuta_rates");
            entity.Property(e => e.Id).HasColumnName("valuta_rates_id");
            entity.Property(e => e.ValutaId).HasColumnName("valuta_id");
            entity.Property(e => e.ValutaCount).HasColumnName("valuta_count");
            entity.Property(e => e.RublesCount).HasColumnName("rubles_count");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");

            entity.HasOne<Valuta>()
                .WithMany()
                .HasForeignKey(e => e.ValutaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Настройка сущности Valuta
        modelBuilder.Entity<Valuta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("valutas");
            entity.Property(e => e.Id).HasColumnName("valuta_id");
            entity.Property(e => e.DigitalCode).HasColumnName("digital_code");
            entity.Property(e => e.LetterCode).HasColumnName("letter_code");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
        });

        // Настройка сущности Operation
        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("operations");
            entity.Property(e => e.Id).HasColumnName("operation_id");
            entity.Property(e => e.CardSenderId).HasColumnName("card_sender_id");
            entity.Property(e => e.CardRecipientId).HasColumnName("card_recipient_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.DateTime).HasColumnName("datetime");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");

            entity.HasOne<Cards.Domain.Entities.Card>()
                .WithMany()
                .HasForeignKey(e => e.CardSenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Cards.Domain.Entities.Card>()
                .WithMany()
                .HasForeignKey(e => e.CardRecipientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Настройка сущности Card
        modelBuilder.Entity<Cards.Domain.Entities.Card>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("cards");
            entity.Property(e => e.Id).HasColumnName("card_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ValutaId).HasColumnName("valuta_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.ExpiredAt).HasColumnName("expired_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Valuta>()
                .WithMany()
                .HasForeignKey(e => e.ValutaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Настройка сущности User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("users");
            entity.Property(e => e.Id).HasColumnName("user_id");
            entity.Property(e => e.Login).HasColumnName("user_name");
            entity.Property(e => e.Email).HasColumnName("user_email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.MiddleName).HasColumnName("middle_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Password).HasColumnName("user_password");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
        });

        // Настройка сущности AccessToken
        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("tokens");
            entity.Property(e => e.Id).HasColumnName("token_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.ExpiredAt).HasColumnName("expired_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}