namespace PrimeValLife.Core.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        public int CartId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        [ForeignKey("User")]
        public int UserId {  get; set; }
        public User User { get; set; }

    }

}
