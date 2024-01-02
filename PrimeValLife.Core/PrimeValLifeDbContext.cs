using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PrimeValLife.Core.Models.Orders;
using PrimeValLife.Core.Models.Others;
using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Core.Models.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUT.IAuth;

namespace PrimeValLife.Core
{
    public class PrimeValLifeDbContext : DbContext
    {
        public PrimeValLifeDbContext(DbContextOptions<PrimeValLifeDbContext> options) : base(options)
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
        public DbSet<ProductPrimaryInfo> ProductPrimaryInfos { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPromotion> UserPromotions { get; set; }
        public DbSet<UserTracking> UserTrackings { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<TempCart> TempCarts { get; set; }
        public DbSet<TempCartItem> TempCartItems { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);
            
        }
        public void SeedData()
        {
           
            var users = new List<User>()
            {
                new User()
                {
                    Username="Arun",
                    Email = "arun@gmail.com",
                    Password="Arun*",
                    UserIdentityId ="0463df1f-a275-4202-ad52-6714f0c3be84"
                }

            };
            if (!Users.Any())
            {
                Users.AddRange(users);
                SaveChanges();
            }
            var address = new List<Address>()
            {
                new Address()
                {
                    AddressLine1 = "Street1",
                    AddressLine2 = "Town1",
                    City = "Test City",
                    ZipCode ="1212121",
                    Phone ="212121212",
                    Country ="India",
                    State ="TN",
                    UserId = Users.First().UserId
                }   

            };
            if (!Addresses.Any())
            {
                Addresses.AddRange(address);
                SaveChanges();
            }
            
            var vendors = new List<Vendor>
            {
                new Vendor(){
                VendorName="PrimeVal",
                VendorDescription="Primary Vendor",
                Average = 5,
                LogoUrl="SOMEURL",
                UserId = Users.First().UserId,
                }
            };
            if (!Vendors.Any())
            {
                Vendors.AddRange(vendors);
                SaveChanges();
            }
            var products = new List<Product>
            {
                new Product { Name = "Product 1" ,SKU="PVL-111-111",Description="Test Description",
                    LongDescription="Test Description",IsOnSale=true,StandardCost=12.00m,
                    Price=10.00m,StockQuantity=10,VendorId=1,
                    ProductPrimaryInfo=new ProductPrimaryInfo(){
                    ProductType = "Grocery",
                    MFG =DateOnly.MinValue,
                    Life=90,
                    Tags=new List<string>() {"fIRST","sECOND","tHIRD"},
                    },

                }
            };
            if (!Products.Any())
            {
                Products.AddRange(products);
                SaveChanges();
            }
            var categories = new List<Category>
            {
                new Category{Name="Category 1"}
            };
            if (!Categories.Any())
            {
                Categories.AddRange(categories);
                SaveChanges();
            }
            var productCategory = new List<ProductCategory>()
            {
                new ProductCategory(){ProductId=Products.First().ProductId,CategoryId=Categories.First().CategoryId},
            };
            if (!ProductCategories.Any())
            {
                ProductCategories.AddRange(productCategory);
                SaveChanges();
            }
            var productImages = new List<ProductImage>()
            {
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-1.jpg"},
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-3.jpg"},
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-4.jpg"},
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-5.jpg"},
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-6.jpg"},
                new ProductImage() {ProductId=1,ImageUrl="assets/imgs/shop/product-16-7.jpg"}
            };
            ProductImages.AddRange(productImages);
            var productVariation = new List<ProductVariation>() {
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "50g",
                ProductId = 1,
            },
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "60g",
                ProductId = 1,
            },
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "70g",
                ProductId = 1,
            },
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "80g",
                ProductId = 1,
            } };
            ProductVariation.AddRange(productVariation);
            var productInfo = new List<ProductInfo>()
            {
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50",
                    ProductId = 1,
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50",
                    ProductId = 1,
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50",
                    ProductId = 1,
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50",
                    ProductId = 1,
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50",
                    ProductId = 1,
                }

            };
            ProductInfos.AddRange(productInfo);
            SaveChanges();
        }


    }
}
