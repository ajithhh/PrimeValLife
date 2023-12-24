namespace PrimeValLife.Core.Models.Products
{
    public class ProductVariation
    {
        public int ProductVariationId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal PriceAdjustment { get; set; }
        public int StockQuantity { get; set; }

        public Product Product { get; set; }
    }

}
