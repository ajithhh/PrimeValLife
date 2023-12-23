using TUT.IAuth.Models;

namespace TUT.IAuth.IServices
{
    public interface IIdentityService
    {
        Task<bool> RegisterUser(PvlUser pvlUser);
    }
}