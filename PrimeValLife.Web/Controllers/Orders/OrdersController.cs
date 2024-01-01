using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Core.Migrations;
using PrimeValLife.Web.Models;
using System.Security.Claims;
using System.Text.Json;

namespace PrimeValLife.Web.Controllers.Orders
{
    public class OrdersController : Controller
    {
        private readonly PrimeValLifeDbContext _context;
        public OrdersController(PrimeValLifeDbContext context)
        {
            _context = context;
        }
        public IActionResult CheckOut([FromQuery] bool directCheckOut=false)
        {   if (directCheckOut)
            {
                var checkOut = TempData["checkOut"];
                return View("OrdersCheckOut",checkOut);
            }
            OrdersCheckOut ordersCheckOut = new OrdersCheckOut();
            var userIdentity = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ordersCheckOut.Cart = _context.Carts.FirstOrDefault(c => c.User.UserIdentity.Id == userIdentity);
            ordersCheckOut.User = ordersCheckOut.Cart.User;
            ordersCheckOut.CartItem = _context.CartItems.Where(ci=>ci.Cart.CartId == ordersCheckOut.Cart.CartId).ToList();
            return View("OrdersCheckOut",ordersCheckOut);
        }

    }
}
