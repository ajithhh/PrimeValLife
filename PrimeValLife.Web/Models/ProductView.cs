using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Vendors;

namespace PrimeValLife.Web.Models
{
    public class ProductView
    {
        public bool Active { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal StandardCost { get; set; }
        public decimal PromoCost { get; set; }
        public Product Product { get; set; }
        public string ProductPrimaryCategory { get; set; }
        public List<Product>? ProductVariation { get; set; }
        public string ProductLongDescription { get; set; }
        public List<ProductInfo> ProductInfo { get; set; }
        public Vendor ProductVendor { get; set; }
        public string SKU { get; set; }
        public List<Review> ProductReview { get; set; }
        public List<Product> ProductsRelated { get; set; }

    }
}
