namespace PrimeValLife.Core.Models.Orders
{
    public class OrderTracking
    {
        public int OrderTrackingId { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }

        public Order Order { get; set; }
    }

}
