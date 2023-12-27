using PrimeValLife.Core.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Users
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }

    }

}
