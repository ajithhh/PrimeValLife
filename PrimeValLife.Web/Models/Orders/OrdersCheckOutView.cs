using PrimeValLife.Core.Models.Users;
using PrimeValLife.Web.Models.Products;

namespace PrimeValLife.Web.Models.Orders
{
    public class OrdersCheckOutView
    {
        public Cart Cart { get; set; }
        public User User { get; set; }
        public Address PrimaryAddress { get; set; }
        public decimal ShippingCharge {  get; set; }
        public List<CartProducts> CartProducts {  get; set; }


    }
}
