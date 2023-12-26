using PrimeValLife.Core.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Orders
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,6)")]
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
