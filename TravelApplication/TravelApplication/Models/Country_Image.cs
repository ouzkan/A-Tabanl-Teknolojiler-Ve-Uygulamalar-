using System.ComponentModel.DataAnnotations;

namespace TravelApplication.Models
{
    public class Country_Image
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Resim yolu gereklidir.")]
        [Display(Name = "Resim Yolu")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Resim açıklaması gereklidir.")]
        [Display(Name = "Resim Açıklaması")]
        [MinLength(10, ErrorMessage = "Açıklama en az 10 karakter olmalıdır.")]
        public string ImageDescription { get; set; }

        [Required(ErrorMessage = "Resim adı gereklidir.")]
        [Display(Name = "Resim Adı")]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "Ülke seçimi gereklidir.")]
        [Display(Name = "Ülke")]
        public int? CountryId { get; set; }

        public Country_Name Country_Name { get; set; }
    }
}
