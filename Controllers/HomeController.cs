using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.Home;
using Portal.Models.Registration;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

         public IActionResult Index()
        {
               return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Registration(RegistrationModelsViewModel? formModel)
        {
            HomeModelsPageModel model = new()
            {
                PageTitle = "Ðåºñòðàö³ÿ",
                FormModel = formModel
            };
            return View(model);
        }

        public IActionResult SignIn()
        {
            HomeModelsPageModel model = new()
            {
                PageTitle = "Âõ³ä",   
            };
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
