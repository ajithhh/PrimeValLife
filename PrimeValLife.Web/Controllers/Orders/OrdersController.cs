using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Web.Models;

namespace PrimeValLife.Web.Controllers.Orders
{
    public class OrdersController : Controller
    {
        private readonly PrimeValLifeDbContext _context;
        public OrdersController(PrimeValLifeDbContext context)
        {
            _context = context;
        }
        public IActionResult CheckOut(OrdersCheckOut checkOut)
        {            
            return View("OrdersCheckOut",checkOut);
        }

    }
}
