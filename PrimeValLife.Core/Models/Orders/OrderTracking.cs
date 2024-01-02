using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Orders
{
    public class OrderTracking
    {
        public int OrderTrackingId { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

}
