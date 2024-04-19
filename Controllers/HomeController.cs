using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.Home;
using Portal.Models.Home.SignUp;
using System.Diagnostics;
using Portal.Data.Dal;
using Portal.Services.Hash;
using Portal.Services.Kdf;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHashService _hashService;
        private readonly IKdfService _kdfService;
        private readonly DataAccessor _dataAccessor;

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


        public IActionResult SignIn()
        {
            HomeModelsPageModel model = new()
            {
                PageTitle = "¬х≥д",   
            };
            return View(model);
        }



        public IActionResult SignUp(SignUpFormModel? formModel)
        {
            SignUpPageModel pageModel = new()
            {
                PageTitle = "–еЇстрац≥€",
                SignUpFormModel = formModel, 
                ValidationErrors = _ValidateSignUpModel(formModel)
            };

            if (formModel?.UserEmail != null) 
            { 
                if (pageModel.ValidationErrors.Any())
                {
                    pageModel.Message = "–еЇстрац≥€ в≥дхилена";
                    pageModel.IsSuccess = false;                        
                }
                else
                {
                    _dataAccessor.UserDao.SignUpUser(mapUser(formModel));
                    pageModel.Message = "–еЇстрац≥€ усп≥шна";
                    pageModel.IsSuccess = true;
                }




            }

            return View(pageModel);
        }


private Data.Entities.User mapUser(SignUpFormModel formModel)
        {
            string salt = Guid.NewGuid().ToString();
            return new()
            {
                Id = new Guid(),
                Name = formModel.UserFirstName,
                SurName = formModel.UserSurName,
                PhoneNumber = formModel.UserPhoneNumber,
                Email = formModel.UserEmail
            };
        }


        private Dictionary<String, String> _ValidateSignUpModel(SignUpFormModel? formModel)
        {
            Dictionary<String, String> res = new(); // перел≥к помилок по форм≥
            if (formModel == null)
            {
                res[nameof(formModel)] = "Model is null";
            }
            else
            {
                if (String.IsNullOrEmpty(formModel.UserFirstName))
                {
                    res[nameof(formModel.UserFirstName)] = "Name is empty";
                }
                if (String.IsNullOrEmpty(formModel.UserEmail))
                {
                    res[nameof(formModel.UserEmail)] = "≈лектронна адреса не задана";
                }
            }
            return res;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
