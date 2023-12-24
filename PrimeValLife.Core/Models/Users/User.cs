namespace PrimeValLife.Core.Models.Users
{
    using PrimeValLife.Core.Models.Orders;
    using System;
    using System.Collections.Generic;

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public Address Address { get; set; }
        public List<UserPromotion> UserPromotions { get; set; }
        public Cart Cart { get; set; }
        public Wishlist Wishlist { get; set; }
        public List<Order> Orders { get; set; }
        public UserTracking UserTracking { get; set; }
    }

}
