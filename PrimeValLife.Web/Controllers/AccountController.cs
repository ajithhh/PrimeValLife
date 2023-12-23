namespace PrimeValLife.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TUT.IAuth.IServices;
using TUT.IAuth.Models;

public class AccountController(IIdentityService identityService, ILogger<AccountController> logger) : Controller
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly IIdentityService _iservice = identityService;

    public IActionResult Register()
    {
        ClaimsPrincipal currentUser = HttpContext.User;

        if (currentUser.Identity.IsAuthenticated)
        {
            string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string username = currentUser.FindFirst(ClaimTypes.Name)?.Value;
            string email = currentUser.FindFirst(ClaimTypes.Email)?.Value;
            return RedirectToAction("Index", "Account");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(PvlUser user)
    {
        var result = await _iservice.RegisterUser(user);
        if (result)
        {
            return RedirectToAction("Register", "Account");
        }
        return View(result);
    }
}

