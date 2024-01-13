using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PrimeValLife.Core;
using PrimeValLife.Web.API.Products.Models;
using System.Text.Json;

namespace PrimeValLife.Web.API.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly PrimeValLifeDbContext _context;
        public ProductsApiController(PrimeValLifeDbContext context)
        {
            _context = context;
        }

        [Route("search")]
        public string GetSearchOption(string query) 
        { 
            var result_items = _context.Products.Select(p=> new { p.Name,p.ProductId,p.PrimaryImageUrl }).Where(p=>p.Name.Contains(query)).ToList();
            SearchSuggetions suggetions = new SearchSuggetions();
            
            suggetions.Suggests = new Dictionary<string, List<Suggest>>();
            List<Suggest> suggests = new List<Suggest>();
            foreach (var item in result_items)
            {
                Suggest suggest = new Suggest(item.Name, null, $"Products/ProductView?id={item.ProductId}");
                suggests.Add(suggest);
            }
            suggetions.Words = suggests;
            List<Suggest> s_suggests = new List<Suggest>();
            foreach (var item in result_items)
            {
                Suggest suggest = new Suggest(item.Name,item.PrimaryImageUrl, $"Products/ProductView?id={item.ProductId}");
                s_suggests.Add(suggest);
            }
            suggetions.Suggests.Add("Products",s_suggests);
            
            
            return JsonSerializer.Serialize(suggetions,new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase});
        }
    }
}
