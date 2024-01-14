using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeValLife.Core;
using PrimeValLife.Web.Models;
using PrimeValLife.Web.Models.Products;
using System.Diagnostics;
using System.Security.Claims;

namespace PrimeValLife.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PrimeValLifeDbContext _context;
        public HomeController(ILogger<HomeController> logger,PrimeValLifeDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = HttpContext.User;

            if (currentUser != null && currentUser.Identity != null && currentUser.Identity.IsAuthenticated)
            {
                string? userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string? username = currentUser.FindFirst(ClaimTypes.Name)?.Value;
                string? email = currentUser.FindFirst(ClaimTypes.Email)?.Value;
            }
            IndexView index = new();
            index.Products = _context.Products.Include(p=>p.ProductCategories)
                                              .ThenInclude(pc=>pc.Category)
                                              .Include(p=>p.ProductVariations)
                                              .ToList();
            return View(index);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}