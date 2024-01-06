namespace PrimeValLife.Web.API.Orders.Models.Common
{
    public class Error
    {
        public Error(string msg, int code)
        {
            ErrorMessage = msg;
            ErrorCode = code;
        }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

    }
}
