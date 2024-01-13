namespace PrimeValLife.Web.API.Account;

using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;

[Route("api/[controller]")]
[ApiController]
public class AccountApiController : ControllerBase
{
    private readonly PrimeValLifeDbContext _context;
    public AccountApiController(PrimeValLifeDbContext context)
    {
        _context = context;
    }
}