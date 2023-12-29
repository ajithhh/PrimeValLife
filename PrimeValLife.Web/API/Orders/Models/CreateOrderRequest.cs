using PrimeValLife.Core.Models.Others;
using PrimeValLife.Web.API.Common;

namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateOrderRequest:Request
    {
        public int UserId {  get; set; }
        public int ProductId { get; set; }
        public string SKU {  get; set; }
        public int Quantity {  get; set; }
        public int shippingAddressId {  get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        
    }
}
