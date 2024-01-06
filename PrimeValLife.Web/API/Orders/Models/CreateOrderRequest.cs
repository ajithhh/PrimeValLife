using PrimeValLife.Core.Models.Others;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Web.API.Orders.Models.Common;

namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateOrderRequest:Request
    {
        public  Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public List<CartItem> CartProducts { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool CreateNewAccount {  get; set; }

        public string PasswordAttached {  get; set; }
        public string Billingfname {  get; set; }
        public string Billinglname { get; set; }
        public string shippingfname { get; set; }
        public string shippinglname { get; set; }
    }
}
