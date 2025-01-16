using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace TravelApplication.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CountryDbContext _context;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, CountryDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Countrys()
        {
            var countries = _context.Country_Names
                .Select(cn => new
                {
                    Country = cn,
                    FirstImage = cn.Country_Images.OrderBy(ci => ci.Id).FirstOrDefault() ?? new Country_Image
                    {
                        ImagePath = "Norwey.png"
                    }
                })
                .ToList();

            return View(countries);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emailSettings = _configuration.GetSection("EmailSettings");
                    
                    // Debug için ayarları kontrol edelim
                    _logger.LogInformation($"SMTP Server: {emailSettings["SmtpServer"]}");
                    _logger.LogInformation($"Port: {emailSettings["Port"]}");
                    _logger.LogInformation($"Username: {emailSettings["Username"]}");
                    
                    var smtpClient = new SmtpClient(emailSettings["SmtpServer"])
                    {
                        Port = int.Parse(emailSettings["Port"]),
                        Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(emailSettings["SenderEmail"], emailSettings["SenderName"]),
                        Subject = "Yeni İletişim Formu Mesajı",
                        Body = $"Gönderen: {model.Name}\nE-posta: {model.Email}\n\nMesaj:\n{model.Message}",
                        To = { emailSettings["Username"] }
                    };

                    smtpClient.Send(mailMessage);

                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi.";
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    // Hatanın detayını loglayalım
                    _logger.LogError($"Mail gönderimi sırasında hata oluştu: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                    }
                    
                    ModelState.AddModelError("", $"Mesaj gönderilirken bir hata oluştu: {ex.Message}");
                }
            }

            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(int id)
        {
            var countryDetails = _context.Country_Names
                .Include(cn => cn.Country_Images)
                .FirstOrDefault(cn => cn.Id == id);

            if (countryDetails == null)
            {
                return NotFound();
            }

            // Her bir görselin açıklamasını markdown formatında işle
            foreach (var image in countryDetails.Country_Images)
            {
                image.ImageDescription = image.ImageDescription?.Trim() ?? "";
                image.ImageName = image.ImageName?.Trim() ?? "";
            }

            // Ülke açıklamasını kontrol et
            countryDetails.CountryDescription = countryDetails.CountryDescription?.Trim() ?? "";

            return View(countryDetails);
        }
    }
}