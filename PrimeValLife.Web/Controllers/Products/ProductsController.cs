using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeValLife.Core;
using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Users;
using PrimeValLife.Core.Models.Vendors;
using PrimeValLife.Web.Models;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PrimeValLife.Web.Controllers.Products
{

    public class ProductsController : Controller
    {
        private readonly PrimeValLifeDbContext _context;
        public ProductsController(PrimeValLifeDbContext primeContext)
        {
            _context = primeContext;
        }
        public async Task<IActionResult> ProductView(int id)
        {
            //_context.SeedData();
            ProductView productView = new ProductView();
            Product product = _context.Products.Include(p => p.ProductVariations)
                                               .Include(p => p.ProductPrimaryInfo)
                                               .Include(p => p.ProductCategories).ThenInclude(c=>c.Category)
                                               .Include(p => p.Vendor)
                                               .ThenInclude(v => v.User)
                                               .ThenInclude(u => u.Address)
                                               .FirstOrDefault(p=>p.ProductId==id)!;
            if (product == null)
            {
                return NotFound();
            }
            productView.Product = product;
            productView.ProductImages = _context.ProductImages.Where(i=>i.ProductId==id).ToList();
            return View(productView);
        }

    }
}
