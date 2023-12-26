using PrimeValLife.Core.Models.Users;

namespace PrimeValLife.Core.Models.Vendors
{
    public class Vendor
    {
        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorDescription { get; set; }

        public string LogoUrl { get; set; }

        public byte Average { get; set; }

        public Address Address { get; set; }
    }
}
