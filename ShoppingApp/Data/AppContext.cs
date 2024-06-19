using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;

namespace ShoppingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Developer> Developers { get; set; } = null!;
        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;

        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Store> Stores { get; set; } = null!;
        public DbSet<StoreItem> StoreItems { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;

        public DbSet<OrderType> OrderTypes { get; set; } = null!;

        public DbSet<PayType> PayTypes { get; set; } = null!;

        
    }
}
