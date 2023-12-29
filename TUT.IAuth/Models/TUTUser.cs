namespace TUT.IAuth.Models;

public class TUTUser
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
    public TUTUser()
    {
        RememberMe = false;
    }
}