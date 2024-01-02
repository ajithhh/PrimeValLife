using PrimeValLife.Core.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Users
{
    public class TempCartItem
    {
        public int TempCartItemId { get; set; }
        [ForeignKey("TempCart")]
        public int TempCartId { get; set; }
        public TempCart TempCart { get; set; }

        [ForeignKey("Product")]
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int Quantity {  get; set; }
    }
}