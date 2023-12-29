namespace PrimeValLife.Web.API.Common
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
