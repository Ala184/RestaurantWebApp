using Microsoft.EntityFrameworkCore;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Infrastructure
{
    public class FoodDeliveryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }  
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public FoodDeliveryDbContext (DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FoodDeliveryDbContext).Assembly);
        }

    }
}
