namespace Ecommerce.Controllers
{
    using Ecommerce.Data;
    using Ecommerce.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Email ve şifre zorunludur.";
                return View();
            }

            if (password != confirmPassword)
            {
                TempData["Error"] = "Şifreler eşleşmiyor.";
                return View();
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == email);
            if (emailExists)
            {
                TempData["Error"] = "Bu email zaten kayıtlı.";
                return View();
            }

            var newUser = new User
            {
                Email = email,
                Password = ComputeSha256Hash(password),
                // Diğer alanlar eklenebilir
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Kayıt başarılı. Lütfen giriş yapınız.";
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Email ve şifre giriniz.";
                return View();
            }

            string hashedPassword = ComputeSha256Hash(password);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                TempData["Message"] = "Giriş başarılı. Hoş geldiniz!";
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Hatalı e-posta veya şifre.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Başarıyla çıkış yapıldı.";
            return RedirectToAction("Index", "Home");
        }

        // **Profil sayfası için yeni action:**
        public async Task<IActionResult> Profile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users
               .Include(u => u.Orders)  
                .FirstOrDefaultAsync(u => u.Id == userId.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(User updatedUser)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null || userId != updatedUser.Id)
                return Unauthorized();

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Phone = updatedUser.Phone;
            user.Address = updatedUser.Address;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Profil güncellendi.";
            return RedirectToAction("Profile");
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
