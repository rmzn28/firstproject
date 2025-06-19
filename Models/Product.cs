using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "REAL")] // SQLite için uygun
        [Range(0.01, 1000000, ErrorMessage = "Fiyat pozitif olmalı")]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // Navigation Property
        public Category Category { get; set; } = null!;
    }
}
