using PrimeValLife.Core.Models.Others;

namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateOrderRequest
    {
        public required OrderAddress BillingAddress { get; set; }
        public required OrderAddress ShippingAddress { get; set; }
        public List<CartProducts> CartProducts { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool? CreateNewAccount {  get; set; }
        public string? PasswordAttached {  get; set; }

        public string? EmailAttached { get; set; }

    }

    public class CartProducts
    {
        public required int ProductId { get; set; }
        public required int Quantity {  get; set; }
    }

    public class OrderAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string FName { get; set; }
        public string  LName{ get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }

    }

   
}
