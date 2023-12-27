using System.Text;

namespace TUT.Payments.PhonePe
{
    public async Task<JsonResult> GeneratePaymentLink(VerifyRequestModel phonePePayment)
        {
            try
            {
                // ON LIVE URL YOU MAY GET CORS ISSUE, ADD Below LINE TO RESOLVE
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var PhonePeGatewayURL = "https://api-preprod.phonepe.com/apis/pg-sandbox";

                var httpClient = new HttpClient();
                var uri = new Uri($"{PhonePeGatewayURL}/pg/v1/pay");

                // Add headers
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("X-VERIFY", phonePePayment.X_VERIFY);

                // Create JSON request body
                var jsonBody = $"{{\"request\":\"{phonePePayment.base64}\"}}";
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Send POST request
                var response = await httpClient.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Return a response
                return Json(new { Success = true, Message = "Verification successful", phonepeResponse = responseContent });
            }
            catch (Exception ex)
            {
                // Handle errors and return an error response
                return Json(new { Success = false, Message = "Verification failed", Error = ex.Message });
            }
        }


        public async Task<JsonResult> CheckPaymentStatus(VerifyRequestModel phonePePayment)
        {
            try
            {
                // ON LIVE URL YOU MAY GET CORS ISSUE, ADD Below LINE TO RESOLVE
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var PhonePeGatewayURL = "https://api-preprod.phonepe.com/apis/pg-sandbox";

                var httpClient = new HttpClient();
                var uri = new Uri($"{PhonePeGatewayURL}/pg/v1/status/{phonePePayment.MERCHANTID}/{phonePePayment.TransactionId}");

                // Add headers
                httpClient.DefaultRequestHeaders.Add("accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("X-VERIFY", phonePePayment.X_VERIFY);
                httpClient.DefaultRequestHeaders.Add("X-MERCHANT-ID", phonePePayment.MERCHANTID);

                // Create JSON request body

                // Send POST request
                var response = await httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Return a response
                return Json(new { Success = true, Message = "Verification successful", phonepeResponse = responseContent });
            }
            catch (Exception ex)
            {
                // Handle errors and return an error response
                return Json(new { Success = false, Message = "Verification failed", Error = ex.Message });
            }
        }

    public class VerifyRequestModel
    {
        public string X_VERIFY { get; set; }
        public string base64 { get; set; }
        public string TransactionId { get; set; }
        public string MERCHANTID { get; set; }
        // Add other properties from the request if needed
    }
}