using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string ReviewVal { get; set;}

        public byte Rating { get; set;}

        [ForeignKey("User")]
        public int Reviewer {  get; set;}

        [ForeignKey("Product")]
        public int ProductId { get; set;}

        public int AssociatedOrderID { get; set;}

    }
}
