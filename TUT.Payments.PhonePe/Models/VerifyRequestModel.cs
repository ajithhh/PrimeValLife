namespace TUT.Payments.PhonePe.Models;

public class VerifyRequestModel
{
    public required string XVerify { get; set; }
    public required string Base64 { get; set; }
    public required string TransactionId { get; set; }
    public required string MerchantId { get; set; }
}