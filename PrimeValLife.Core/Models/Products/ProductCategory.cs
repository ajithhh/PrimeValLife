using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeValLife.Core.Models.Products
{
    [PrimaryKey(nameof(Product.ProductId),nameof(Category.CategoryId))]
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Category Category { get; set; }
    }

}
