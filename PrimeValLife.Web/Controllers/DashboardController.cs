namespace PrimeValLife.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Web.Models;
using PrimeValLife.Web.Models.Dashboard;
using System.Diagnostics;
using System.Security.Claims;
using PrimeValLife.Core.Utilities;

public class DashboardController(ILogger<DashboardController> logger, PrimeValLifeDbContext context) : Controller
{
    private readonly ILogger<DashboardController> _logger = logger;
    private readonly PrimeValLifeDbContext _context = context;
    private readonly DbUtilities? _utils = new(context);

    public IActionResult Index()
    {
        ClaimsPrincipal currentUser = HttpContext.User;
        string? aspId = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int id = 0;
        if (string.IsNullOrWhiteSpace(aspId) || _utils == null)
        {
            RedirectToAction("Index", "Home");
        }
        else
        {
            id = _utils.GetUserId(aspId);
        }
        var query = _context.Orders.Where(o => o.UserId == id)
                    .Join(_context.OrderItems, o => o.OrderId, oi => oi.OrderId, (o, oi) => new { o, oi })
                    .GroupBy(g => new
                    {
                        g.o.OrderId,
                        g.o.OrderDate,
                        g.o.Status,
                        g.o.UserId,
                        g.o.PaymentMethod,
                        g.o.PaymentAuthorization
                    })
                    .OrderByDescending(grouped => grouped.Key.OrderDate)
                    .Select(grouped => new
                    {
                        grouped.Key.OrderId,
                        grouped.Key.OrderDate,
                        grouped.Key.Status,
                        grouped.Key.UserId,
                        grouped.Key.PaymentMethod,
                        grouped.Key.PaymentAuthorization,
                        TotalPrice = grouped.Sum(x => x.oi.Price),
                        TotalQuantity = grouped.Sum(x => x.oi.Quantity)
                    });

        var result = query.ToList();

        var viewModelList = result.Select(item => new OrderViewModel
        {
            OrderId = item.OrderId,
            OrderDate = item.OrderDate,
            Status = item.Status,
            UserId = item.UserId,
            PaymentMethod = item.PaymentMethod,
            PaymentAuthorization = item.PaymentAuthorization,
            TotalPrice = item.TotalPrice,
            TotalQuantity = item.TotalQuantity
        }).ToList();
        return View(viewModelList);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
