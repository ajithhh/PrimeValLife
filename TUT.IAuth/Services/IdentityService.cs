namespace TUT.IAuth.Services;

using Azure;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TUT.IAuth.IServices;
using TUT.IAuth.Models;
using TUT.Utilities.Models;

public class IdentityService(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager) : IIdentityService
{
    private readonly UserManager<IdentityUser> userManager = userManager;
    private readonly SignInManager<IdentityUser> signInManager = signInManager;

    public async Task<ResponseItem<string>> RegisterUser(TUTUser tutUser)
    {
        ResponseItem<string> response = new();
        IdentityUser user = new()
        {
            UserName = tutUser.UserName,
            Email = tutUser.UserName
        };
        var result = await userManager.CreateAsync(user, tutUser.Password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            response.Success = true;
        }
        if(result.Errors.Any())
        {
            response.Errors = result.Errors.First();
        }
        return response;
    }

    public async Task SignOut()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<ResponseItem<string>> SignIn(TUTUser tutUser)
    {
        ResponseItem<string> response = new();
        IdentityUser user = new()
        {
            UserName = tutUser.UserName,
        };
        var result = await signInManager.PasswordSignInAsync(user.UserName, tutUser.Password, false, false);
        if (result.Succeeded)
        {
            response.Success = true;
        }
        //if (result.)
        //{
        //    response.Errors = result.Errors.First();
        //}
        return response;
    }

    public bool IsSignedIn(ClaimsPrincipal claims)
    {
        return signInManager.IsSignedIn(claims);
    }
}