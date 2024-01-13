namespace PrimeValLife.Core.Utilities;

public class DbUtilities(PrimeValLifeDbContext context)
{
    private readonly PrimeValLifeDbContext _context = context;

    #region Get UserId
    public int GetUserId(string aspId)
    {
        var user = _context.Users
            .Where(u => u.UserIdentityId == aspId)
            .FirstOrDefault();
        return user?.UserId ?? 0;
    }
    #endregion
}