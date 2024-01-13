namespace PrimeValLife.Web.Models.Products
{
    public class Filter
    {
        public int MinPrice {  get; set; }
        public int MaxPrice { get; set; }

        public SortBy SortBy { get; set; }
    }

    public enum SortBy
    {
        Featured,
        PriceHighToLow,
        PriceLowToHigh,
        Rating
    }
}