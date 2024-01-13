namespace PrimeValLife.Core.Models.Others;

using System.ComponentModel;

public enum PaymentAuthorization
{
    [Description("COD")]
    COD,
    [Description("PREPAID INITIATED")]
    PREPAID_INITIATED,
    [Description("PREPAID PENDING")]
    PREPAID_PENDING,
    [Description("PREPAID REJECTED")]
    PREPAID_REJECTED,
    [Description("PREPAID AUTHORIZED")]
    PREPAID_AUTHORIZED
}