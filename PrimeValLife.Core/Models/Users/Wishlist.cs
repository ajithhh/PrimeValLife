namespace PrimeValLife.Core.Models.Users
{
    using System.Collections.Generic;

    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public List<WishlistItem> WishlistItems { get; set; }
    }

}
