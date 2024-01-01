using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Vendors;

namespace PrimeValLife.Web.Models
{
    public class ProductView
    {
        public Product Product { get; set; }
        public string ProductPrimaryCategory { get; set; }
        public List<Review> ProductReview { get; set; }
        public List<Product> ProductsRelated { get; set; }
        public List<ProductImage> ProductImages { get; set; }


    }
}
