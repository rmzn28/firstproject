namespace Ecommerce.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Þifre gereklidir.")]
        [MinLength(6, ErrorMessage = "Þifre en az 6 karakter olmalýdýr.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Þifre tekrarý gereklidir.")]
        [Compare("Password", ErrorMessage = "Þifreler eþleþmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
namespace Ecommerce.Controllers
{
    using Ecommerce.Models; // RegisterViewModel için gerekli using eklendi  
}
