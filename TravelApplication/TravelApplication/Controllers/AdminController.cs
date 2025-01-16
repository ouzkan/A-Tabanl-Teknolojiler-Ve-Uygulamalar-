using Microsoft.AspNetCore.Mvc;
using TravelApplication.Models;

namespace TravelApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly CountryDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(CountryDbContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCountry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var country = new Country_Name
            {
                CountryName = model.CountryName,
                CountryDescription = model.CountryDescription
            };

            _context.Country_Names.Add(country);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddImages()
        {
            // Ülkeleri ViewBag ile gönder
            ViewBag.Countries = _context.Country_Names.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddImages(Country_Image model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Countries = _context.Country_Names.ToList();
                return View(model);
            }

            // Markdown formatında örnek açıklama
            model.ImageDescription = @"# Fuji Dağı

## Genel Bilgiler
* Yükseklik: 3,776 metre
* Konum: Honshu Adası
* Tür: Aktif stratovolkan

## Kültürel Önemi
* Japonya'nın en yüksek dağı
* UNESCO Dünya Mirası Listesi'nde
* Japon sanatının önemli sembollerinden

### Ziyaret Bilgileri
Tırmanış sezonu Temmuz-Ağustos ayları arasındadır.";

            _context.Country_Images.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



    }



}
