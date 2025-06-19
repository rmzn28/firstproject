﻿namespace Ecommerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public int? UserId { get; internal set; }
        public User User { get; set; } = null!;
    }
}
