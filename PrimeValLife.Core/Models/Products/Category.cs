namespace PrimeValLife.Core.Models.Products
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public  List<ProductCategory> ProductCategories { get; set; }
    }

}
