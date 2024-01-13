namespace PrimeValLife.Web.API.Products.Models
{
    public class Suggest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        // Add more fields if needed

        public Suggest(string name, string image, string link = null)
        {
            Name = name;
            Image = image;
            Link = link;
        }
    }
}