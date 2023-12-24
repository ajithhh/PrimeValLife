using Microsoft.EntityFrameworkCore;
using PrimeValLife.Core.Models.Orders;
using PrimeValLife.Core.Models.Others;
using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUT.IAuth;

namespace PrimeValLife.Core
{
    public class PrimeValLifeDbContext:DbContext
    {
        public PrimeValLifeDbContext(DbContextOptions<PrimeValLifeDbContext> options) :base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderTracking> OrderTracking { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductVariation> ProductVariation { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPromotion> UserPromotions { get; set; }
        public DbSet<UserTracking> UserTrackings { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

    }
}
