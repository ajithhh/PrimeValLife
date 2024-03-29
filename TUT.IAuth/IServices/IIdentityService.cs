﻿namespace TUT.IAuth.IServices;

using System.Security.Claims;
using TUT.IAuth.Models;
using TUT.Utilities.Models;

public interface IIdentityService
{
    Task<ResponseItem<string>> RegisterUser(TUTUser tutUser);
    Task<ResponseItem<string>> SignIn(TUTUser user);
    Task SignOut();
    bool IsSignedIn(ClaimsPrincipal claims);
}