using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    public class ProductPrimaryInfo
    {
        public int ProductPrimaryInfoId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string ProductType { get; set; }

        public DateOnly MFG { get; set; }

        public int Life {  get; set; } // in days

        public List<string> Tags { get; set; }
    }
}
