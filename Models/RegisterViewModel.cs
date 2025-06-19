namespace Ecommerce.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Ge�erli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "�ifre gereklidir.")]
        [MinLength(6, ErrorMessage = "�ifre en az 6 karakter olmal�d�r.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "�ifre tekrar� gereklidir.")]
        [Compare("Password", ErrorMessage = "�ifreler e�le�miyor.")]
        public string ConfirmPassword { get; set; }
    }
}
namespace Ecommerce.Controllers
{
    using Ecommerce.Models; // RegisterViewModel i�in gerekli using eklendi  
}
