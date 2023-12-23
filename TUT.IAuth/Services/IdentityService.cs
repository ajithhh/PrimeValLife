namespace TUT.IAuth.Services;

using Microsoft.AspNetCore.Identity;
using TUT.IAuth.IServices;
using TUT.IAuth.Models;

public class IdentityService(UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager) : IIdentityService
{
    private readonly UserManager<IdentityUser> userManager = userManager;
    private readonly SignInManager<IdentityUser> signInManager = signInManager;

    public async Task<bool> RegisterUser(PvlUser pvlUser)
    {
        IdentityUser user = new()
        {
            UserName = pvlUser.UserName,
            Email = pvlUser.UserName
        };
        var result = await userManager.CreateAsync(user, pvlUser.Password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }
        return false;
    }
}