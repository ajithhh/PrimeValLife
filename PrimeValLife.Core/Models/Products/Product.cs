namespace PrimeValLife.Core.Models.Products
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductVariation> ProductVariations { get; set; }

    }

}
