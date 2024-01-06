using PrimeValLife.Core.Models.Others;
using PrimeValLife.Web.API.Orders.Models.Common;
namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateShippingResponse:Response
    {
        public int ShippingAddressId {  get; set; }
        public bool Available { get; set; }
        //public DateTime EstimatedDelivery {  get; set; }
    }
}
