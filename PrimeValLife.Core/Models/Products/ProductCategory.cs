using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    [PrimaryKey(nameof(Product.ProductId),nameof(Category.CategoryId))]
    public class ProductCategory
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }

}
