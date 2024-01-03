namespace PrimeValLife.Web.Models.Products
{
    public class CartProducts
    {
        public int ProrductId {  get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice {  get; set; }

        public int CartProductQuantity { get; set; } 

        public int StockProductQuantity {  get; set; }

        public string ProductDescription { get; set; }

        public string ProductImgUrl {  get; set; }

        public int AverageReview {  get; set; }
    }
}
