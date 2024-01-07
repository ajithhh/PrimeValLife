namespace TUT.Payments.PhonePe.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text;
using TUT.Payments.PhonePe.Models;
using TUT.Utilities.Models;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ResponseItem<object>> GeneratePaymentLink(VerifyRequestModel phonePePayment)
    {
        ResponseItem<object> responseItem = new();
        try
        {
            // ON LIVE URL YOU MAY GET CORS ISSUE, ADD Below LINE TO RESOLVE
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var phonePeGatewayURL = "https://api-preprod.phonepe.com/apis/pg-sandbox";

            var httpClient = new HttpClient();
            var uri = new Uri($"{phonePeGatewayURL}/pg/v1/pay");

            // Add headers
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("X-VERIFY", phonePePayment.XVerify);

            // Create JSON request body
            var jsonBody = $"{{\"request\":\"{phonePePayment.Base64}\"}}";
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Send POST request
            var response = await httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response content
            var responseContent = await response.Content.ReadAsStringAsync();

            responseItem.Success = true;
            responseItem.Message = "Verification Successful";
            responseItem.Data = responseContent;
            return responseItem;
        }
        catch (Exception ex)
        {
            responseItem.Message = "Verification Failed";
            responseItem.Errors = ex;
            return responseItem;
        }
    }


    //public async Task<JsonResult> CheckPaymentStatus(VerifyRequestModel phonePePayment)
    //{
    //    try
    //    {
    //        // ON LIVE URL YOU MAY GET CORS ISSUE, ADD Below LINE TO RESOLVE
    //        //ServicePointManager.Expect100Continue = true;
    //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

    //        var PhonePeGatewayURL = "https://api-preprod.phonepe.com/apis/pg-sandbox";

    //        var httpClient = new HttpClient();
    //        var uri = new Uri($"{PhonePeGatewayURL}/pg/v1/status/{phonePePayment.MERCHANTID}/{phonePePayment.TransactionId}");

    //        // Add headers
    //        httpClient.DefaultRequestHeaders.Add("accept", "application/json");
    //        httpClient.DefaultRequestHeaders.Add("X-VERIFY", phonePePayment.X_VERIFY);
    //        httpClient.DefaultRequestHeaders.Add("X-MERCHANT-ID", phonePePayment.MERCHANTID);

    //        // Create JSON request body

    //        // Send POST request
    //        var response = await httpClient.GetAsync(uri);
    //        response.EnsureSuccessStatusCode();

    //        // Read and deserialize the response content
    //        var responseContent = await response.Content.ReadAsStringAsync();

    //        // Return a response
    //        return Json(new { Success = true, Message = "Verification successful", phonepeResponse = responseContent });
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle errors and return an error response
    //        return Json(new { Success = false, Message = "Verification failed", Error = ex.Message });
    //    }
    //}

}
