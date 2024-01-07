using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    public class ProductVariation
    {
        public int ProductVariationId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string VariationType {  get; set; }
        public string VariationValue { get; set; }
        public List<ProductImage> ImageUrls { get; set; }

    }

}
