using Microsoft.EntityFrameworkCore;
using Common.Abstracts;
using Users.Domain.Entities;
using Access.Domain.Entities;
using Operations.Domain.Entities;

namespace App.Infrastructure.Data;

#pragma warning disable 1591
public class ApplicationDbContext : DbContext, IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Operation> Operations { get; set; } = default!;
    public DbSet<Card.Domain.Entities.Card> Cards { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<AccessToken> AccessTokens { get; set; } = default!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
        });

        // Настройка сущности Card
        modelBuilder.Entity<Card.Domain.Entities.Card>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("cards");
            entity.Property(e => e.Id).HasColumnName("card_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.ExpiredAt).HasColumnName("expired_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
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
        });
    }
}