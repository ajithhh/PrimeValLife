namespace PrimeValLife.Core.Models.Others
{
    using PrimeValLife.Core.Models.Users;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Promotion
    {
        public int PromotionId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(10,6)")]
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
      
    }

}
