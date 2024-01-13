namespace PrimeValLife.Web.Models.Dashboard;

using PrimeValLife.Core.Models.Orders;
using PrimeValLife.Core.Models.Others;

public class OrderViewModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public int UserId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentAuthorization PaymentAuthorization { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalQuantity { get; set; }
}
