using ScaleStore.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ScaleStore.Infrastructure.Data
{
    public class ScaleStoreDbContext : DbContext
    {
        public ScaleStoreDbContext(DbContextOptions<ScaleStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
