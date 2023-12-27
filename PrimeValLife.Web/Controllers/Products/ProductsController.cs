using Microsoft.AspNetCore.Mvc;
using PrimeValLife.Core.Models.Products;
using PrimeValLife.Core.Models.Vendors;
using PrimeValLife.Web.Models;

namespace PrimeValLife.Web.Controllers.Products
{

    public class ProductsController : Controller
    {
        public IActionResult ProductView()
        {
            ProductView productView = new ProductView();
            Product product = new Product();
            product.ProductCategories = new List<ProductCategory>();
            product.ProductCategories.Add(new ProductCategory()
            {
                Category = new Category()
                {
                    Name = "TestCategory"
                }
            });
            productView.Product = product;
            productView.Product.StockQuantity = 5;
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
            productView.ProductLongDescription = "<div class=\"\">\r\n                                                    <p>Uninhibited carnally hired played in whimpered dear gorilla koala depending and much yikes off far quetzal goodness and from for grimaced goodness unaccountably and meadowlark near unblushingly crucial scallop tightly neurotic hungrily some and dear furiously this apart.</p>\r\n                                                    <p>Spluttered narrowly yikes left moth in yikes bowed this that grizzly much hello on spoon-fed that alas rethought much decently richly and wow against the frequent fluidly at formidable acceptably flapped besides and much circa far over the bucolically hey precarious goldfinch mastodon goodness gnashed a jellyfish and one however because.</p>\r\n                                                    <ul class=\"product-more-infor mt-30\">\r\n                                                        <li><span>Type Of Packing</span> Bottle</li>\r\n                                                        <li><span>Color</span> Green, Pink, Powder Blue, Purple</li>\r\n                                                        <li><span>Quantity Per Case</span> 100ml</li>\r\n                                                        <li><span>Ethyl Alcohol</span> 70%</li>\r\n                                                        <li><span>Piece In One</span> Carton</li>\r\n                                                    </ul>\r\n                                                    <hr class=\"wp-block-separator is-style-dots\">\r\n                                                    <p>Laconic overheard dear woodchuck wow this outrageously taut beaver hey hello far meadowlark imitatively egregiously hugged that yikes minimally unanimous pouted flirtatiously as beaver beheld above forward energetic across this jeepers beneficently cockily less a the raucously that magic upheld far so the this where crud then below after jeez enchanting drunkenly more much wow callously irrespective limpet.</p>\r\n                                                    <h4 class=\"mt-30\">Packaging &amp; Delivery</h4>\r\n                                                    <hr class=\"wp-block-separator is-style-wide\">\r\n                                                    <p>Less lion goodness that euphemistically robin expeditiously bluebird smugly scratched far while thus cackled sheepishly rigid after due one assenting regarding censorious while occasional or this more crane went more as this less much amid overhung anathematic because much held one exuberantly sheep goodness so where rat wry well concomitantly.</p>\r\n                                                    <p>Scallop or far crud plain remarkably far by thus far iguana lewd precociously and and less rattlesnake contrary caustic wow this near alas and next and pled the yikes articulate about as less cackled dalmatian in much less well jeering for the thanks blindly sentimental whimpered less across objectively fanciful grimaced wildly some wow and rose jeepers outgrew lugubrious luridly irrationally attractively dachshund.</p>\r\n                                                    <h4 class=\"mt-30\">Suggested Use</h4>\r\n                                                    <ul class=\"product-more-infor mt-30\">\r\n                                                        <li>Refrigeration not necessary.</li>\r\n                                                        <li>Stir before serving</li>\r\n                                                    </ul>\r\n                                                    <h4 class=\"mt-30\">Other Ingredients</h4>\r\n                                                    <ul class=\"product-more-infor mt-30\">\r\n                                                        <li>Organic raw pecans, organic raw cashews.</li>\r\n                                                        <li>This butter was produced using a LTG (Low Temperature Grinding) process</li>\r\n                                                        <li>Made in machinery that processes tree nuts but does not process peanuts, gluten, dairy or soy</li>\r\n                                                    </ul>\r\n                                                    <h4 class=\"mt-30\">Warnings</h4>\r\n                                                    <ul class=\"product-more-infor mt-30\">\r\n                                                        <li>Oil separation occurs naturally. May contain pieces of shell.</li>\r\n                                                    </ul>\r\n                                                </div>";
            productView.ProductInfo = new List<ProductInfo>()
            {
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50"
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50"
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50"
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50"
                },
                new ProductInfo()
                {
                    ProductInfoName = "Size",
                    ProductInfoValue = "50 x 50"
                }

            };
            productView.PrimaryInfo = new ProductPrimaryInfo()
            {
                ProductType = "Orgainc",
                MFG = DateOnly.MinValue,
                Life = 90,
                Tags = new List<string>()
                {
                    "Snacks","Food","Grocerires"
                },

            };
            productView.ProductVendor = new Vendor() 
            { Address =new Core.Models.Users.Address() { City = "Chennai",Street= "Main Street",Country="India",State="TN",ZipCode="600032"},
              LogoUrl = "assets/imgs/vendor/vendor-18.svg" ,
              VendorName = "Test Vendor",
              VendorDescription = "Noodles & Company is an American fast-casual restaurant that offers international and American noodle dishes and pasta in addition to soups and salads. Noodles & Company was founded in 1995 by Aaron Kennedy and is headquartered in Broomfield, Colorado. The company went public in 2013 and recorded a $457 million revenue in 2017.In late 2018, there were 460 Noodles & Company locations across 29 states and Washington, D.C."
            };

            return View(productView);
        }
    }
}
