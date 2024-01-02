using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    public class ProductPrimaryInfo
    {
        [Key,ForeignKey("Product")]
        public int ProductPrimaryInfoId { get; set; }
        public string ProductType { get; set; }
        public DateOnly MFG { get; set; }
        public int Life {  get; set; } // in days
        public List<string> Tags { get; set; }
        public Product Product { get; set; }
    }
}
