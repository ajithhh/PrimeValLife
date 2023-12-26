namespace PrimeValLife.Core.Models.Products
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [PrimaryKey(nameof(ProductId))]
    public class Product
    {
        public int ProductId { get; set; }   
        public string Name { get; set; }
        public string SKU {  get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(10,6)")]
        public decimal StandardCost {get; set; }
        [Column(TypeName = "decimal(10,6)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProductInfo ProductInfo { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductVariation> ProductVariations { get; set; }

    }

}
