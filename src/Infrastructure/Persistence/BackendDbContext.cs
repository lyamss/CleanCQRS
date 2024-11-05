using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
    public sealed class BackendDbContext(DbContextOptions<BackendDbContext> options) : DbContext(options), IBackendDbContext
    {
        internal DbSet<User> Users { get; set; }

        internal DbSet<Items> Items { get; set; }

        internal DbSet<Transaction> Transactions { get; set; }

        internal DbSet<AuthToken> AuthTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany(u => u.AuthToken)
               .WithOne(au => au.User)
               .HasForeignKey(au => au.IdUser)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transaction)
                .WithOne(tr => tr.User)
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TransactionItems>()
                       .HasKey(ti => new { ti.TransactionId, ti.ItemsId });

            modelBuilder.Entity<TransactionItems>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.TransactionItems)
                .HasForeignKey(ti => ti.TransactionId);

            modelBuilder.Entity<TransactionItems>()
                .HasOne(ti => ti.Items)
                .WithMany(i => i.TransactionItems)
                .HasForeignKey(ti => ti.ItemsId);
        }

        public void Migrate()
        {
            this.Database.Migrate();
        }
    }

    public interface IBackendDbContext
    {
        void Migrate();
    }
}