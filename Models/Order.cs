using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }  
        public System.Collections.Generic.List<OrderItem> Items { get; set; } = null!;
        [Required]
        [Column(TypeName = "REAL")] // SQLite için uygun
        public double Total { get; set; }
        public string Status { get; set; } = "";
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
