using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TravelApplication.Models
{
    public class Country_Name
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ülke adı gereklidir.")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Ülke açıklaması gereklidir.")]
        public string CountryDescription { get; set; }

        [ValidateNever]
        public ICollection<Country_Image> Country_Images { get; set; } = new List<Country_Image>();
    }
}