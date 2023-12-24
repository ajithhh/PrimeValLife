using PrimeValLife.Core.Models.Products;

namespace PrimeValLife.Core.Models.Users
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public int WishlistId { get; set; }
        public int ProductId { get; set; }

        public Wishlist Wishlist { get; set; }
        public Product Product { get; set; }
    }

}
