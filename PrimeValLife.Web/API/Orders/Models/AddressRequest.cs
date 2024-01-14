using PrimeValLife.Core.Models.Users;

namespace PrimeValLife.Web.API.Orders.Models
{
    public class AddressRequest
    {
        public int AddressId {  get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public AddressType AddressType { get; set; }
        public string AddressLine1 {  get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

    }
}
