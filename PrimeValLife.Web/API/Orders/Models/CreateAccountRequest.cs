namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateAccountRequest
    {
        public required string Email {  get; set; }
        public required string Password { get; set; }
    }
}
