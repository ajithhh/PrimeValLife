using Microsoft.AspNetCore.Mvc;

namespace PrimeValLife.Web.Controllers.Products
{

    public class ProductsController : Controller
    {
        public IActionResult ProductView()
        {
            return View("ProductView");
        }
    }
}
