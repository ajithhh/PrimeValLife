using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
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
        {   
            return View();
        }

    }
}
