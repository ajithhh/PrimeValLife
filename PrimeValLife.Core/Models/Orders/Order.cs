namespace PrimeValLife.Core.Models.Orders
{
    using PrimeValLife.Core.Models.Others;
    using PrimeValLife.Core.Models.Users;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }        
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentAuthorization PaymentAuthorization { get; set; }
    }

}
