namespace PrimeValLife.Core.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        public int CartId { get; set; }

        public User User { get; set; }

    }

}
