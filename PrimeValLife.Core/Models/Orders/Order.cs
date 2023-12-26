namespace PrimeValLife.Core.Models.Orders
{
    using PrimeValLife.Core.Models.Users;
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderTracking OrderTracking { get; set; }
    }

}
