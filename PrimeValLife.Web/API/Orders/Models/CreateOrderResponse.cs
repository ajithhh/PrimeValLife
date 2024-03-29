﻿using PrimeValLife.Core.Models.Others;
using PrimeValLife.Web.API.Orders.Models.Common;
namespace PrimeValLife.Web.API.Orders.Models
{
    public class CreateOrderResponse:Response
    {
        public int OrderId {  get; set; }
        public string OrderReference
        {
            get
            {
                return  $"PVL-{CustomerId}-{OrderId}";
            }
        }
             
        public int CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentAuthorization Authorization { get; set; }

        //public DateTime EstimatedDelivery {  get; set; }
    }
}
