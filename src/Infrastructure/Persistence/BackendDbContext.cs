using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
    public sealed class BackendDbContext(DbContextOptions<BackendDbContext> options) : DbContext(options), IBackendDbContext
    {
        internal DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}