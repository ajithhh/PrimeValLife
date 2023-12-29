namespace TUT.IAuth.IServices;
using TUT.IAuth.Models;
using TUT.Utilities.Models;

public interface IIdentityService
{
    Task<ResponseItem<string>> RegisterUser(TUTUser tutUser);
    Task<ResponseItem<string>> SignIn(TUTUser user);
    Task SignOut();
}