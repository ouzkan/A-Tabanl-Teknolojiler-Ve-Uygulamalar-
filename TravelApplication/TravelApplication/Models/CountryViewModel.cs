using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace TravelApplication.Models
{
    public class CountryViewModel
    {
        [Required(ErrorMessage = "Ülke adı gereklidir.")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Ülke açıklaması gereklidir.")]
        public string CountryDescription { get; set; }
    }
}
