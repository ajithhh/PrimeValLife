using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Core.Models.Orders;
using PrimeValLife.Web.API.Orders.Models;

namespace PrimeValLife.Web.API.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly PrimeValLifeDbContext _context;
        public OrdersApiController(PrimeValLifeDbContext context)
        {
            _context = context;
        }

        public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                Order order = new Order();
                order.OrderDate = request.RequestTime;
                order.UserId = request.UserId;
                order.PaymentMethod = request.PaymentMethod;
                order.Status = "NEW";
                order.PaymentAuthorization = (request.PaymentMethod == Core.Models.Others.PaymentMethod.COD) ? Core.Models.Others.PaymentAuthorization.COD : Core.Models.Others.PaymentAuthorization.PREPAID_INITIATED;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                var product = await _context.Products.FindAsync(request.ProductId);
                decimal price = product?.Price ?? 0; // Assuming Price is a decimal property; provide a default value if needed
                OrderItem item = new OrderItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Price = price,
                    OrderId = order.OrderId
                };
                _context.OrderItems.Add(item);
                await _context.SaveChangesAsync();
                if (order.OrderId != 0)
                {
                    return new CreateOrderResponse()
                    {
                        OrderId = order.OrderId,
                        Authorization = order.PaymentAuthorization

                    };
                }
                else
                {
                    return new CreateOrderResponse()
                    {
                        Error = new Common.Error("Order Not Created", 1)
                    };

                }
            }
            catch (Exception ex)
            {
                return new CreateOrderResponse() {
                    Error = new Common.Error("UnExpected Error Occured.Contact DEV TEAM", 0)
                };

            }
        }
        public async Task<CreateShippingResponse> CreateShippingInfo(CreateShippingRequest request)
        {
            try
            {
                var matchedAddress = _context.Addresses.SingleOrDefault(address =>
     address.AddressLine1.ToLower().Trim() == request.ShippingAddress.AddressLine1.ToLower().Trim() &&
     address.AddressLine2.ToLower().Trim() == request.ShippingAddress.AddressLine2.ToLower().Trim() &&
     address.City.ToLower().Trim() == request.ShippingAddress.City.ToLower().Trim() &&
     address.State.ToLower().Trim() == request.ShippingAddress.State.ToLower().Trim() &&
     address.ZipCode.ToLower().Trim() == request.ShippingAddress.ZipCode.ToLower().Trim());
                if(matchedAddress == null || matchedAddress.AddressId==0)
                {
                    _context.Addresses.Add(request.ShippingAddress);
                    _context.SaveChanges();
                }
                matchedAddress = request.ShippingAddress;
                return new CreateShippingResponse()
                {
                    ShippingAddressId = matchedAddress.AddressId,
                    Available = true
                };
            }
            catch (Exception ex)
            {
                return new CreateShippingResponse()
                {
                    Error = new Common.Error("UnExpected Error Occured.Contact DEV TEAM", 0)
                };

            }
        }

    }
}
