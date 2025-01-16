using System.ComponentModel.DataAnnotations;

namespace TravelApplication.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Ad Soyad gereklidir.")]
        [Display(Name = "Ad Soyad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mesaj gereklidir.")]
        [Display(Name = "Mesajınız")]
        public string Message { get; set; }
    }
} 