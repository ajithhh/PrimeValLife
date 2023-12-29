using PrimeValLife.Core.Models.Others;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Web.API.Common;

namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateShippingRequest:Request
    {
        public int UserId {  get; set; }
        public Address ShippingAddress { get; set; }
        
    }
}
