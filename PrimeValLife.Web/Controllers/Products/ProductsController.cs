using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core.Models.Products;
using PrimeValLife.Web.Models;

namespace PrimeValLife.Web.Controllers.Products
{

    public class ProductsController : Controller
    {
        public IActionResult ProductView()
        {
            ProductView productView =new ProductView();
            Product product= new Product();
            product.ProductCategories = new List<ProductCategory>();
            product.ProductCategories.Add(new ProductCategory()
            {
                Category = new Category()
                {
                    Name = "TestCategory"
                }
            });
            productView.Product = product;
            productView.ProductName = "Test Name";//Redundant use product
            productView.ProductImages = new List<ProductImage>()
            {
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"},
                new ProductImage() {ImageUrl="assets/imgs/shop/product-16-2.jpg"}
            };
            productView.Product.Price = 80;
            productView.Product.StandardCost = 100;
            productView.ProductDescription = "This is is a test description please focus on next steps";
            productView.ProductVariation = new List<ProductVariation>() { 
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "50g"
            },
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "60g"
            },
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "70g"
            },
            new ProductVariation()
            {
                VariationType = "Size/Weight",
                VariationValue = "80g"
            }

            };

            return View(productView);
        }
    }
}
