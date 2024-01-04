using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Web.Models;
using System.Security.Claims;
using System.Text.Json;

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
        public IActionResult OrdersCheckOut()
        {   
            return View();
        }

    }
}
