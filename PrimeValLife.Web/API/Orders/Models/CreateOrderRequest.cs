using PrimeValLife.Core.Models.Others;

namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateOrderRequest
    {
        public required int BillingAddress { get; set; }
        public required int ShippingAddress { get; set; }
        public List<CartProducts> CartProducts { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }

    public class CartProducts
    {
        public required int ProductId { get; set; }
        public required int Quantity {  get; set; }
    }

   
}
