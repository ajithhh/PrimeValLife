using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Web.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PrimeValLife.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            return View();
        }

        public IActionResult Privacy()
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