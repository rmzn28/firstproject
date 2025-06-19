using Ecommerce.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// MVC desteği
builder.Services.AddControllersWithViews();

// Session ve HttpContext erişimi
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Veritabanı bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=MiniEcommerce.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
 


// Hata ayıklama (opsiyonel - geliştirme ortamında önerilir)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTPS yönlendirmesi
app.UseStaticFiles();      // wwwroot klasöründeki statik dosyaları kullan

app.UseRouting();          // Routing sistemi aktif

app.UseSession();          // Session middleware’i

app.UseAuthorization();    // Yetkilendirme kontrolü

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
