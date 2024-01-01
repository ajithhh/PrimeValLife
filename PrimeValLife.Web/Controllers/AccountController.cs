namespace PrimeValLife.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TUT.IAuth.IServices;
using TUT.IAuth.Models;
using TUT.Utilities.Models;

[AllowAnonymous]
public class AccountController(IIdentityService identityService, ILogger<AccountController> logger) : Controller
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly IIdentityService _iservice = identityService;

    public IActionResult Register()
    {
        ClaimsPrincipal currentUser = HttpContext.User;

        if (currentUser != null && currentUser.Identity != null && currentUser.Identity.IsAuthenticated)
        {
            string? userId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? username = currentUser.FindFirst(ClaimTypes.Name)?.Value;
            string? email = currentUser.FindFirst(ClaimTypes.Email)?.Value;
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public IActionResult Login(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<ResponseItem<string>> Register([FromBody] TUTUser user)
    {
        ResponseItem<string> response = new();
        var result = await _iservice.RegisterUser(user);
        if (result.Success)
        {
            response.Success = true;
        }
        if (result.Errors != null)
        {
            response.Success = false;
            response.Errors = result.Errors;
        }
        return response;
    }

    [HttpPost]
    public async Task<ResponseItem<string>> SignIn([FromBody] TUTUser user)
    {
        ResponseItem<string> response = new();
        var result = await _iservice.SignIn(user);
        if (result.Success)
        {
            response.Success = true;
        }
        if (result.Errors != null)
        {
            response.Success = false;
            response.Errors = result.Errors;
        }
        return response;
    }

    public async Task<IActionResult> Logout()
    {
        await _iservice.SignOut();
        return RedirectToAction("Index", "Home");
    }
}

