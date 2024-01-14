namespace PrimeValLife.Core.Models.Orders;

using PrimeValLife.Core.Models.Others;
using PrimeValLife.Core.Models.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Order
{
    public Order()
    {
        this.OrderDate = DateTime.Now;
    }
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }       
    public User User { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentAuthorization PaymentAuthorization { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public List<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();

    [ForeignKey("Address")]
    public int BillingAddressId {  get; set; }

    [ForeignKey("Address")]
    public int ShippingAddressId { get; set; }

    public  Address BillingAddress { get; set; }
    public  Address ShippingAddress { get; set; }

}