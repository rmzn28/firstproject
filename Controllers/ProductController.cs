using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string search, int? categoryId)
    {
        var products = _context.Products.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            products = products.Where(p => p.Name.Contains(search));
        }

        if (categoryId.HasValue && categoryId.Value != 0)
        {
            products = products.Where(p => p.CategoryId == categoryId.Value);
        }

        ViewBag.Categories = _context.Categories.ToList(); // Dropdown için

        // View dosyasının tam yolunu belirtiyoruz
        return View("~/Views/ProductCategory/Index.cshtml", products.ToList());
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        return View(product);
    }
}
