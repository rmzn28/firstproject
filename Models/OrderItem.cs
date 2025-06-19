namespace Ecommerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }           // FK tanımı
        public Order Order { get; set; } = null!;           // Navigasyon properti

        public int ProductId { get; set; }         // FK tanımı
        public Product Product { get; set; } = null!;       // Navigasyon properti

        public int Quantity { get; set; }
    }
}
