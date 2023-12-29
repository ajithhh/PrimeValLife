using PrimeValLife.Core.Models.Users;

namespace PrimeValLife.Web.Models
{
    public class OrdersCheckOut
    {
       public Cart Cart { get; set; }
       public User User { get; set; }
       public List<CartItem> CartItem { get; set; }

    }
}
