using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    public class ProductInfo
    {
        public int ProductInfoId {  get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ProductInfoName { get; set; }
        public string ProductInfoValue { get; set; }

    }
}
