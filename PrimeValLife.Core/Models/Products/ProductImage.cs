using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeValLife.Core.Models.Products
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string ImageUrl { get; set; }

    }
}
