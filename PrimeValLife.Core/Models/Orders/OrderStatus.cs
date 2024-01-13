namespace PrimeValLife.Core.Models.Orders;

using System.ComponentModel;

public enum OrderStatus
{
    [Description("NEW")]
    NEW,
    [Description("PENDING")]
    PENDING,
    [Description("APPROVED")]
    APPROVED,
    [Description("SHIPPED")]
    SHIPPED,
    [Description("DELIVERED")]
    DELIVERED,
    [Description("CANCELLED")]
    CANCELLED
}