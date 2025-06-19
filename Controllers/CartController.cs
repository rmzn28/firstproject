using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Account");

        var cart = _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.UserId == userId)
            .ToList();

        ViewBag.Total = cart.Sum(item => item.Product.Price * item.Quantity);
        return View(cart);
    }

    [HttpPost]
    public IActionResult Add(int productId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Account");

        var product = _context.Products.Find(productId);
        if (product == null) return NotFound();

        var existing = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            var cartItem = new CartItem
            {
                ProductId = product.Id,
                Quantity = 1,
                UserId = userId.Value
            };
            _context.CartItems.Add(cartItem);
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var item = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

        if (item != null)
        {
            _context.CartItems.Remove(item);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Decrease(int productId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var item = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

        if (item != null)
        {
            if (item.Quantity > 1)
                item.Quantity--;
            else
                _context.CartItems.Remove(item);

            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Checkout()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["Error"] = "Sipariş verebilmek için giriş yapmalısınız.";
            return RedirectToAction("Login", "Account");
        }

        var cart = _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.UserId == userId)
            .ToList();

        if (cart.Count == 0) return RedirectToAction("Index");

        // Her ürün gerçekten var mı kontrol et
        foreach (var item in cart)
        {
            if (!_context.Products.Any(p => p.Id == item.ProductId))
            {
                TempData["Error"] = $"HATA: Sepette geçersiz ürün var. (ProductId = {item.ProductId})";
                return RedirectToAction("Index");
            }
        }

        // 1. Order'ı oluştur ve önce kaydet
        var order = new Order
        {
            OrderDate = DateTime.Now,
            Status = "Onay Bekliyor",
            Total = cart.Sum(i => (double)(i.Product.Price * i.Quantity)),
            UserId = userId.Value
        };
        _context.Orders.Add(order);
        _context.SaveChanges(); // Order.Id burada oluşur

        // 2. OrderItem'ları oluştur
        foreach (var item in cart)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id, // foreign key artık hazır
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
            _context.OrderItems.Add(orderItem);
        }

        // 3. Cart temizle + OrderItem'ları kaydet
        _context.CartItems.RemoveRange(cart);
        _context.SaveChanges(); // Artık foreign key hatası almazsın

        return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
    }
    [HttpGet]
    public IActionResult OrderConfirmation(int orderId)
    {
        var order = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null) return NotFound();

        return View(order);
    }

    
    [HttpPost]
    public IActionResult ProceedToPayment(int orderId)
    {
        var order = _context.Orders.Find(orderId);
        if (order == null) return NotFound();

        order.Status = "Ödeme Bekleniyor";
        _context.SaveChanges();

        return RedirectToAction("PaymentPage", new { orderId = order.Id });
    }
    public IActionResult PaymentPage(int orderId)
    {
        var order = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null) return NotFound();

        return View(order);
    }
    [HttpPost]
    public IActionResult CompletePayment(int orderId)
    {
        var order = _context.Orders.Find(orderId);
        if (order == null) return NotFound();

        order.Status = "Ödeme Tamamlandı";
        _context.SaveChanges();

        return RedirectToAction("OrderSuccess", new { orderId = order.Id });
    }
    public IActionResult OrderSuccess(int orderId)
    {
        var order = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null) return NotFound();

        return View(order);
    }

}
