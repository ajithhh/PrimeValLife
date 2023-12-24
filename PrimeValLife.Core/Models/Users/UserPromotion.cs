using Microsoft.EntityFrameworkCore;
using PrimeValLife.Core.Models.Others;

namespace PrimeValLife.Core.Models.Users
{
    [PrimaryKey(nameof(UserId),nameof(PromotionId))]
    public class UserPromotion
    {
        public int UserId { get; set; }
        public int PromotionId { get; set; }

        public User User { get; set; }
        public Promotion Promotion { get; set; }
    }

}
