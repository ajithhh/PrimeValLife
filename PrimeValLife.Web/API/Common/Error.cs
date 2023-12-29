namespace PrimeValLife.Web.API.Common
{
    public class Error
    {
        public Error(string msg,int code)
        {
            this.ErrorMessage = msg;
            this.ErrorCode = code;
        }
        public string ErrorMessage {  get; set; }
        public int ErrorCode { get; set; }

    }
}
