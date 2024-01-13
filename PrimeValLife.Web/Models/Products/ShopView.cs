using PrimeValLife.Core.Models.Products;

namespace PrimeValLife.Web.Models.Products
{
    public class ShopView
    {
        public  string Query {  get; set; }

        public int PageLength {  get; set; }

        public int PageCount { get; set; }

        public int CurrentPage { get; set; }

        public List<Product> Results { get; set; }
        public int TotalCount { get; set; }
       
    }
}
