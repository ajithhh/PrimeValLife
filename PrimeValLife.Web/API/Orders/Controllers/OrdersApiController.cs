using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Core.Models.Orders;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Web.API.Orders.Models;
using PrimeValLife.Web.Controllers;
using System.Security.Claims;
using TUT.IAuth.IServices;
using TUT.IAuth.Models;
using TUT.Utilities.Models;

namespace PrimeValLife.Web.API.Orders.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase
    {
        private readonly PrimeValLifeDbContext _context;
        public OrdersApiController(PrimeValLifeDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ResponseItem<CreateOrderResponse>> CreateOrder([FromBody]CreateOrderRequest request)
        {
            Order order = new Order();
            User user;
            string userIdentityId = null;
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            else
            {
                var _iservice = HttpContext.RequestServices.GetService<IIdentityService>();
                var _ilogger = HttpContext.RequestServices.GetService<ILogger<AccountController>>();
                AccountController accountController = new AccountController(_iservice, _ilogger);
                TUTUser TUTuser = new TUTUser() { UserName = request.EmailAttached, Password = request.PasswordAttached };

                var accountResult = await accountController.Register(TUTuser);
                if (accountResult.Success)
                {
                    userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    user =new User() { Email = request.EmailAttached ,Username=request.EmailAttached!,Password=request.PasswordAttached!,UserIdentityId=userIdentityId!};
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cannot Create Order");
                }

            }
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in request.CartProducts)
            {
                orderItems.Add(new OrderItem() { ProductId = item.ProductId, Quantity = item.Quantity, Price = _context.Products.First(p => p.ProductId == item.ProductId).Price * item.Quantity });
            }
            ResponseItem<CreateOrderResponse> result;
            try
            {
                order.OrderDate = DateTime.Now;
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;
                order.UserId = user.UserId;
                order.OrderItems = orderItems;
                order.PaymentMethod = request.PaymentMethod;
                order.Status = OrderStatus.NEW;
                order.PaymentAuthorization = (request.PaymentMethod == Core.Models.Others.PaymentMethod.COD) ? Core.Models.Others.PaymentAuthorization.COD : Core.Models.Others.PaymentAuthorization.PREPAID_INITIATED;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                foreach (var item in order.OrderItems)
                {
                        CartItem cartItem = _context.CartItems.FirstOrDefault(ci=>ci.ProductId == item.ProductId)!;
                    _context.Remove(cartItem);
                }
                await _context.SaveChangesAsync();
                result = new ResponseItem<CreateOrderResponse>() { Success = true, Data = new CreateOrderResponse() { OrderId = order.OrderId, CustomerId = user.UserId, PaymentMethod = order.PaymentMethod, Authorization = order.PaymentAuthorization } };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResponseItem<CreateOrderResponse>() { Success = false, Data = new CreateOrderResponse() { } ,Errors =ex.Message};
            }
            return null;
        }
        //   public async Task<CreateShippingResponse> CreateShippingInfo(CreateShippingRequest request)
        //   {
        //       try
        //       {
        //           var matchedAddress = _context.Addresses.SingleOrDefault(address =>
        //address.AddressLine1.ToLower().Trim() == request.ShippingAddress.AddressLine1.ToLower().Trim() &&
        //address.AddressLine2.ToLower().Trim() == request.ShippingAddress.AddressLine2.ToLower().Trim() &&
        //address.City.ToLower().Trim() == request.ShippingAddress.City.ToLower().Trim() &&
        //address.State.ToLower().Trim() == request.ShippingAddress.State.ToLower().Trim() &&
        //address.ZipCode.ToLower().Trim() == request.ShippingAddress.ZipCode.ToLower().Trim());
        //           if (matchedAddress == null || matchedAddress.AddressId == 0)
        //           {
        //               _context.Addresses.Add(request.ShippingAddress);
        //               _context.SaveChanges();
        //           }
        //           matchedAddress = request.ShippingAddress;
        //           return new CreateShippingResponse()
        //           {
        //               ShippingAddressId = matchedAddress.AddressId,
        //               Available = true
        //           };
        //       }
        //       catch (Exception ex)
        //       {
        //           return new CreateShippingResponse()
        //           {
        //               Error = new Models.Common.Error("UnExpected Error Occured.Contact DEV TEAM", 0)
        //           };

        //       }
        //   }



        [HttpPost("AddToCart")]
        public async Task<ResponseItem<AddToCartResponse>> AddToCart([FromBody] AddToCartRequest request)
        {
            ResponseItem<AddToCartResponse> response;
            //Check For Authorized User

            var userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                User user;
                Cart cart;
                CartItem item;
                //Authenticated User
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;
                cart = _context.Carts.FirstOrDefault(c => c.UserId == user.UserId)!;
                if (cart == null)
                {
                    cart = new Cart() { UserId = user.UserId, CartItems = new List<CartItem>() };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }
                item = _context.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId && ci.CartId == cart.CartId)!;
                if (item != null)
                {
                    item.Quantity += request.Quantity;
                }
                else
                {
                    item = new CartItem() { ProductId = request.ProductId, Quantity = request.Quantity };
                    cart.CartItems.Add(item!);
                }
                try
                {
                    await _context.SaveChangesAsync();
                    response = new ResponseItem<AddToCartResponse>() { Data = new AddToCartResponse() { CartItemId = item!.CartItemId }, Success = item.CartItemId > 0 };
                }
                catch (Exception ex)
                {
                    response = new ResponseItem<AddToCartResponse>() { Data = new AddToCartResponse() { CartItemId = -1 }, Success = false, Errors = ex, Message = "Unable to add to cart" };
                }
            }
            else
            {
                // Anonymous User
                TempCart tempCart;
                TempCartItem temCartitem = new TempCartItem() { ProductId = request.ProductId, Quantity = request.Quantity };
                List<TempCartItem> tempCartItems = new List<TempCartItem>();
                string anonymousUserId;
                if (HttpContext.Request.Cookies["GuestId"] == null)
                {
                    anonymousUserId = Guid.NewGuid().ToString();
                    HttpContext.Response.Cookies.Append("GuestId", anonymousUserId, new CookieOptions
                    {
                        // Additional cookie options can go here
                        Expires = DateTimeOffset.Now.AddDays(7), // Cookie expiration time
                        HttpOnly = false, // Whether the cookie is accessible through JavaScript
                        Secure = true, // Whether the cookie is sent only over HTTPS
                        SameSite = SameSiteMode.Strict, // Set the SameSite attribute
                    });
                    tempCartItems.Add(temCartitem);
                    tempCart = new TempCart() { SessionId = anonymousUserId, TempCartItems = tempCartItems };
                    _context.TempCarts.Add(tempCart);
                    _context.SaveChanges();
                }
                else
                {
                    anonymousUserId = HttpContext.Request.Cookies["GuestId"]!;
                    tempCart = _context.TempCarts.FirstOrDefault(tc => tc.SessionId == anonymousUserId)!;
                    temCartitem = _context.TempCartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId && tempCart.TempCartId == ci.TempCartId)!;
                    if (temCartitem != null)
                    {
                        temCartitem.Quantity += request.Quantity;

                    }
                    else
                    {
                        temCartitem = new TempCartItem() { ProductId = request.ProductId, Quantity = request.Quantity };
                        tempCart.TempCartItems = [temCartitem!];
                    }
                }
                try
                {

                    await _context.SaveChangesAsync();
                    response = new ResponseItem<AddToCartResponse>() { Data = new AddToCartResponse() { CartItemId = temCartitem!.TempCartItemId }, Success = temCartitem.TempCartItemId > 0 };
                }
                catch (Exception ex)
                {
                    response = new ResponseItem<AddToCartResponse>() { Data = new AddToCartResponse() { CartItemId = -1 }, Success = false, Errors = ex, Message = "Unable to add to cart" };
                }
            }
            return response;

        }


    }
}
