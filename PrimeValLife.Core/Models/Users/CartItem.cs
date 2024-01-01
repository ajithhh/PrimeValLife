using PrimeValLife.Core.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Users
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }

}
