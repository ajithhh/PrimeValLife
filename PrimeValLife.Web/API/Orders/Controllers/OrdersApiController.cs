using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeValLife.Core;
using PrimeValLife.Core.Models.Orders;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Core.Utilities;
using PrimeValLife.Web.API.Orders.Models;
using PrimeValLife.Web.Controllers;
using System.Net;
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
        [Authorize]
        public async Task<ResponseItem<CreateOrderResponse>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            Order order = new Order();
            User user;
            string userIdentityId = null;
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
                order.BillingAddressId = request.BillingAddress;
                order.ShippingAddressId = request.ShippingAddress;
                order.OrderItems = orderItems;
                order.PaymentMethod = request.PaymentMethod;
                order.Status = OrderStatus.NEW;
                order.PaymentAuthorization = (request.PaymentMethod == Core.Models.Others.PaymentMethod.COD) ? Core.Models.Others.PaymentAuthorization.COD : Core.Models.Others.PaymentAuthorization.PREPAID_INITIATED;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                foreach (var item in order.OrderItems)
                {
                    CartItem cartItem = _context.CartItems.Include(ci=>ci.Cart).FirstOrDefault(ci => ci.Cart.UserId == user.UserId && ci.ProductId==item.ProductId )!;
                    _context.Remove(cartItem);
                }
                await _context.SaveChangesAsync();
                result = new ResponseItem<CreateOrderResponse>() { Success = true, Data = new CreateOrderResponse() { OrderId = order.OrderId, CustomerId = user.UserId, PaymentMethod = order.PaymentMethod, Authorization = order.PaymentAuthorization } };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResponseItem<CreateOrderResponse>() { Success = false, Data = new CreateOrderResponse() { }, Errors = ex.Message };
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
        [Route("GetActiveCartItems")]
        public async Task<ResponseItem<List<CartItem>>> GetActiveCartItems()
        {

            User user = null;
            string userIdentityId = null;
            Cart cart = null;
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;
                cart = _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefault(c => c.UserId == user.UserId)!;
                if (cart == null)
                {
                    cart = new Cart() { UserId = user.UserId, CartItems = new List<CartItem>() };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                TempCart tempCart;
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
                    tempCart = new TempCart() { SessionId = anonymousUserId };
                    _context.TempCarts.Add(tempCart);
                    _context.SaveChanges();
                    return new ResponseItem<List<CartItem>>() { Success = true, Data = new List<CartItem>() };
                }
                else
                {
                    anonymousUserId = HttpContext.Request.Cookies["GuestId"]!;
                    tempCart = _context.TempCarts.Include(tc => tc.TempCartItems).ThenInclude(tci => tci.Product).FirstOrDefault(tc => tc.SessionId == anonymousUserId)!;

                }
                cart = TempCart2Cart.ConvertTempCart2Cart(tempCart);

            }
            return new ResponseItem<List<CartItem>>() { Success = true, Data = cart.CartItems };
        }

        [HttpDelete("RemoveItemFromCart")]
        public async Task<ResponseItem<dynamic>> RemoveItemFromCart(int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var item = await _context.CartItems.FindAsync(id);
                if (item != null)
                {
                    _context.CartItems.Remove(item);
                    _context.SaveChanges();
                }
                return new ResponseItem<dynamic> { Success = true, Data = null };

            }
            else
            {
                var item = await _context.TempCartItems.FindAsync(id);
                if (item != null)
                {
                    _context.TempCartItems.Remove(item);
                    _context.SaveChanges();
                }
                return new ResponseItem<dynamic> { Success = true, Data = null };
            }

        }

        [Authorize]
        [Route("getAddress")]
        public async Task<ResponseItem<Address>> getAddress(int id)
        {
            User user = null;
            string userIdentityId = null;
            Address address = null;
            if (User.Identity.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;

                address = _context.Addresses.FirstOrDefault(a => a.AddressId == id && a.UserId == user.UserId);
            }
            return new ResponseItem<Address> { Success = true, Data = address };
        }

        [Authorize]
        [Route("getUserAddress")]
        public async Task<ResponseItem<List<Address>>> getUserAddress()
        {
            User user = null;
            string userIdentityId = null;
            List<Address> addresses = null;
            if (User.Identity.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;

                addresses = _context.Addresses.Where(a => a.UserId == user.UserId).ToList();
            }
            return new ResponseItem<List<Address>> { Success = true, Data = addresses };

        }

        [Authorize]
        [HttpPost("saveAddress")]
        public async Task<ResponseItem<dynamic>> SaveAddress(AddressRequest addressRequest)
        {
            User user = null;
            Address address = new Address();
            string userIdentityId = null;
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;
                address.AddressType = addressRequest.AddressType;
                address.FName = addressRequest.FName;
                address.LName = addressRequest.LName;
                address.AddressLine1 = addressRequest.AddressLine1;
                address.AddressLine2 = addressRequest.AddressLine1;
                address.City = addressRequest.City;
                address.State = addressRequest.State;
                address.Country = "INDIA";
                address.ZipCode = addressRequest.ZipCode;
                address.Phone = addressRequest.Phone;
                address.UserId = user.UserId;

                if (addressRequest.AddressId > 0)
                {
                    var existingAddress = _context.Addresses.FirstOrDefault(a => a.AddressId == addressRequest.AddressId && a.UserId == user.UserId);
                    if (existingAddress != null)
                    {
                        existingAddress.FName = address.FName;
                        existingAddress.LName = address.LName;
                        existingAddress.AddressLine1 = address.AddressLine1;
                        existingAddress.AddressLine2 = address.AddressLine2;
                        existingAddress.City = address.City;
                        existingAddress.State = address.State;
                        existingAddress.Country = address.Country;
                        existingAddress.ZipCode = address.ZipCode;
                        existingAddress.Phone = address.Phone;
                        existingAddress.UserId = address.UserId;
                        _context.SaveChanges();
                    }

                }
                else if (address.AddressId == 0)
                {
                    address.UserId = user.UserId;
                    _context.Add(address);
                    _context.SaveChanges();
                }
                return new ResponseItem<dynamic> { Success = true, Data = null };
            }
            return new ResponseItem<dynamic> { Success = false, Data = null };
        }

        [Route("CreateAccount")]
        public async Task<ResponseItem<dynamic>> CreateAccount(CreateAccountRequest request)
        {
            var _iservice = HttpContext.RequestServices.GetService<IIdentityService>();
            var _ilogger = HttpContext.RequestServices.GetService<ILogger<AccountController>>();
            string userIdentityId;
            User user;
            AccountController accountController = new AccountController(_iservice, _ilogger);
            TUTUser TUTuser = new TUTUser() { UserName = request.Email, Password = request.Password };
            var accountResult = await accountController.Register(TUTuser);
            if (accountResult.Success)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = new User() { Email = request.Email, Username = request.Email!, Password = request.Password!, UserIdentityId = userIdentityId! };
                _context.Users.Add(user);
                _context.SaveChanges();
                return new ResponseItem<dynamic> { Success = true, Data = null };
            }
            return new ResponseItem<dynamic> { Success = false, Data = null };

        }
    }
}
