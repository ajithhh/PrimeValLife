namespace PrimeValLife.Core.Models.Users
{
    using System.Collections.Generic;

    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

}
