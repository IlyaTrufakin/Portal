using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.Home;
using Portal.Models.Home.SignUp;
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

        public IActionResult SignUp(SignUpFormModel? formModel)
        {
            SignUpPageModel pageModel = new()
            {
                PageTitle = "Ðåºñòðàö³ÿ",
                SignUpFormModel = formModel
            };
            return View(pageModel);
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
