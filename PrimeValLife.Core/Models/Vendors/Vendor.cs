using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Vendors
{
    public class Vendor
    {
        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorDescription { get; set; }

        public string LogoUrl { get; set; }

        public byte Average { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        [ForeignKey("User")]
        public int UserId {  get; set; }
        public User User { get; set; }
    }
}
