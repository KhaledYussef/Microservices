using Microsoft.EntityFrameworkCore;

using ShoppingCartApi.Models;

namespace ShoppingCartApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }


        // on model creating add the seed data of coupons
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

    }
}
