namespace PrimeValLife.Web.API.Products.Models
{
    public class SearchSuggetions
    {
        public List<Suggest> Words { get; set; }
        public Dictionary<string, List<Suggest>> Suggests { get; set; }

    }
}