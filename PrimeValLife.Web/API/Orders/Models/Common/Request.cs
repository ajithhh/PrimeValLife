namespace PrimeValLife.Web.API.Orders.Models.Common
{
    public class Request
    {
        public DateTime RequestTime
        {
            get
            {
                return DateTime.Now;
            }
        }
        public Guid RequestId
        {
            get
            {
                return new Guid();
            }
        }
    }
}
