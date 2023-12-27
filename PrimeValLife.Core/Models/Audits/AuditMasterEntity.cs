namespace PrimeValLife.Core.Models.Audits;
public class AuditMasterEntity
{
    public int Id { get; set; }
    public string? ActionId { get; set; }
    public string? Type { get; set; }
    public string? Action { get; set; }
    public string? ActionValue { get; set; }
    public string? ActionBy { get; set; }
    public string? Status { get; set; }
    public DateTime Timestamp { get; set; }
}