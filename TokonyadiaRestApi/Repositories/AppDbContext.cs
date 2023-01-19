using Microsoft.EntityFrameworkCore;
using TokonyadiaRestApi.Entities;

namespace TokonyadiaRestApi.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Store> Stores => Set<Store>();

        protected AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(customer => customer.Email).IsUnique();
                entity.HasIndex(customer => customer.PhoneNumber).IsUnique();
            });
        }

    }
}
