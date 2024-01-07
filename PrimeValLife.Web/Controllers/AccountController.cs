namespace PrimeValLife.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Web.Models.Account;
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

    public IActionResult Login()
    {
        return View();
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
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
    {
        TUTUser tutUser = new() { UserName = model.Email, Password = model.Password, RememberMe = model.RememberMe };
        if (ModelState.IsValid)
        {
            var result = await _iservice.SignIn(tutUser);

            if (result.Success)
            {
                return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction("index", "home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _iservice.SignOut();
        return RedirectToAction("Index", "Home");
    }
}

