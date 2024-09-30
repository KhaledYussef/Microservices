using CouponApi.Models;

using Microsoft.EntityFrameworkCore;

namespace CouponApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }


        // on model creating add the seed data of coupons
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    Code = "10OFF",
                    Discount = 10,
                    MinAmount = 100
                },
                new Coupon
                {
                    Id = 2,
                    Code = "20OFF",
                    Discount = 20,
                    MinAmount = 200
                }
            );
        }

    }
}
