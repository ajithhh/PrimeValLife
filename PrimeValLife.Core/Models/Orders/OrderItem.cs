﻿using PrimeValLife.Core.Models.Products;

namespace PrimeValLife.Core.Models.Orders
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}