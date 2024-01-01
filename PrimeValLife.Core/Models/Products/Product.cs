namespace PrimeValLife.Core.Models.Products
{
    using Microsoft.EntityFrameworkCore;
    using PrimeValLife.Core.Models.Vendors;
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
        public bool IsOnSale { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
       
        [Column(TypeName = "decimal(10,6)")]
        public decimal StandardCost {get; set; }
        [Column(TypeName = "decimal(10,6)")]
        
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Vendor")]
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public ProductPrimaryInfo ProductPrimaryInfo { get; set; }
        public List<ProductInfo> ProductInfo { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductVariation> ProductVariations { get; set; }

    }

}
