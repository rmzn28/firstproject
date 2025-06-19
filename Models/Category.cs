using Ecommerce.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }=null!;
        public List<Product> Products { get; set; } =null!;
    }

}
