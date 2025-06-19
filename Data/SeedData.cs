using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        if (!context.Categories.Any())
        {
            var categories = new[]
            {
            new Category { Name = "Ayakkabı" },
            new Category { Name = "Telefon" },
            new Category { Name = "Oyun Konsolu" },
            new Category { Name = "Kitap" },
            new Category { Name = "Atıştırmalık" },
            new Category { Name = "Bakım Ürünü" },
            new Category { Name = "Giyim" }
        };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        var dbCategories = context.Categories.ToList();

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    Name = "Nike Air Max",
                    Description = "Konforlu spor ayakkabı.",
                    Price = 2999,
                    ImageUrl = "/images/nike.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Ayakkabı").Id
                },
                new Product
                {
                    Name = "iPhone 15",
                    Description = "Yeni nesil akıllı telefon.",
                    Price = 49999,
                    ImageUrl = "/images/iphone.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Telefon").Id
                },
                new Product
                {
                    Name = "PlayStation 5",
                    Description = "Yeni nesil oyun konsolu.",
                    Price = 18999,
                    ImageUrl = "/images/ps5.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Oyun Konsolu").Id
                },
                new Product
                {
                    Name = "1984 - George Orwell",
                    Description = "Dünya çapında bir klasik.",
                    Price = 99,
                    ImageUrl = "/images/kitap.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Kitap").Id
                },
                new Product
                {
                    Name = "Doritos Baharatlı",
                    Description = "Baharatlı tortilla cipsi.",
                    Price = 35,
                    ImageUrl = "/images/cips.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Atıştırmalık").Id
                },
                new Product
                {
                    Name = "Parfüm",
                    Description = "Erkek bakım ürünü.",
                    Price = 59,
                    ImageUrl = "/images/parfüm.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Bakım Ürünü").Id
                },
                new Product
                {
                    Name = "Tişört",
                    Description = "Günlük siyah tişört.",
                    Price = 59,
                    ImageUrl = "/images/tişört.jpg",
                    CategoryId = dbCategories.First(c => c.Name == "Giyim").Id
                }
            );
            context.SaveChanges();
        }
    }

}
