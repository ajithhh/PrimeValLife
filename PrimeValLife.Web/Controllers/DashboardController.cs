namespace PrimeValLife.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core;
using PrimeValLife.Web.Models;
using PrimeValLife.Web.Models.Dashboard;
using System.Diagnostics;
using System.Security.Claims;

public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly PrimeValLifeDbContext _context;

    public DashboardController(ILogger<DashboardController> logger, PrimeValLifeDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var query = _context.Orders.Where(o => o.UserId == 4)
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
