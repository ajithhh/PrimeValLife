using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeValLife.Core;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Core.Utilities;
using PrimeValLife.Web.Models.Orders;
using System.Security.Claims;

namespace PrimeValLife.Web.Controllers.Orders
{
    [AllowAnonymous]
    public class OrdersController : Controller
    {
        private readonly PrimeValLifeDbContext _context;
        public OrdersController(PrimeValLifeDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OrdersCheckOut()
        {
            OrdersCheckOutView checkOutView = new OrdersCheckOutView();

            User user=null;
            string userIdentityId = null;
            Cart cart = null;
            if (HttpContext.User.Identity!.IsAuthenticated)
            {
                userIdentityId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                user = _context.Users.FirstOrDefault(u => u.UserIdentityId == userIdentityId)!;
                cart = _context.Carts.Include(c=>c.CartItems).ThenInclude(ci=>ci.Product).FirstOrDefault(c => c.UserId == user.UserId)!;
                if (cart == null)
                {
                    cart = new Cart() { UserId = user.UserId, CartItems = new List<CartItem>() };
                    _context.Carts.Add(cart);
                    await _context.SaveChangesAsync();
                }
                checkOutView.User = user;
                checkOutView.Cart = cart;
                if (user != null)
                {
                    checkOutView.BillingAddress = _context.Addresses.FirstOrDefault(a => a.UserId == user.UserId && a.AddressType == AddressType.BILLING);
                    checkOutView.ShippingAddress = _context.Addresses.FirstOrDefault(a => a.UserId == user.UserId && a.AddressType == AddressType.SHIPPING);
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
                    checkOutView.Cart = cart;
                    checkOutView.User = user!;
                    return View(checkOutView);

                }
                else
                {
                    anonymousUserId = HttpContext.Request.Cookies["GuestId"]!;
                    tempCart = _context.TempCarts.Include(tc=>tc.TempCartItems).ThenInclude(tci=>tci.Product).FirstOrDefault(tc => tc.SessionId == anonymousUserId)!;
                    
                }
                cart = TempCart2Cart.ConvertTempCart2Cart(tempCart);                
                checkOutView.Cart = cart;
                checkOutView.User = user!;
                if (user!=null)
                {
                    checkOutView.BillingAddress = _context.Addresses.FirstOrDefault(a => a.UserId == user.UserId && a.AddressType == AddressType.BILLING);
                    checkOutView.ShippingAddress = _context.Addresses.FirstOrDefault(a => a.UserId == user.UserId && a.AddressType == AddressType.SHIPPING);
                }
                
            }
            return View(checkOutView);
        }

    }
}
