namespace PrimeValLife.Core.Models.Users
{
    public class UserTracking
    {
        public int UserTrackingId { get; set; }
        public int UserId { get; set; }
        public string PageVisited { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
    }

}
