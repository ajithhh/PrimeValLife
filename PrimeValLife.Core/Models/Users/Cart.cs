namespace PrimeValLife.Core.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        public int CartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

    }

}
