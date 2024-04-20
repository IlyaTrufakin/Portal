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

        public HomeController(ILogger<HomeController> logger, 
                                //IHashService hashService, 
                                IKdfService kdfService, 
                                DataAccessor dataAccessor)
        {
            _logger = logger;
           // _hashService = hashService;
            _kdfService = kdfService;
            _dataAccessor = dataAccessor;
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


        public ViewResult SignIn()
        {
            HomeModelsPageModel model = new()
            {
                PageTitle = "����",
            };
            return View(model);
        }



        public ViewResult SignUp(SignUpFormModel? formModel)
        {
            SignUpPageModel pageModel = new()
            {
                PageTitle = "���������",
                SignUpFormModel = formModel,
                ValidationErrors = _ValidateSignUpModel(formModel)
            };

            if (formModel?.UserEmail != null)
            {
                if (pageModel.ValidationErrors.Any())
                {
                    pageModel.Message = "��������� ��������";
                    pageModel.IsSuccess = false;
                }
                else
                {
                    _dataAccessor.UserDao.SignUpUser(mapUser(formModel));
                    pageModel.Message = "��������� ������";
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
                Id = Guid.NewGuid(),
                UserName = formModel.UserName,
                UserSurName = formModel.UserSurName,
                UserPhoneNumber = formModel.UserPhoneNumber,
                UserEmail = formModel.UserEmail,
                UserCountry = formModel.UserCountry,
                UserRegion = formModel.UserRegion,
                UserLocality = formModel.UserLocality,
                UserAddress1 = formModel.UserAddress1,
                UserAddress2 = formModel.UserAddress2,
                UserInteractionForm = formModel.UserInteractionForm,
                UserCompanyName = formModel.UserCompanyName,
                UserRegistered = DateTime.Now,
                Salt = salt,
                DerivedKey = _kdfService.GetDerivedKey(formModel.UserPassword, salt)
            };
        }


        private Dictionary<String, String> _ValidateSignUpModel(SignUpFormModel? formModel)
        {
            Dictionary<String, String> res = new(); // ������ ������� �� ����
            if (formModel == null)
            {
                res[nameof(formModel)] = "Model is null";
            }
            else
            {
                if (String.IsNullOrEmpty(formModel.UserName))
                {
                    res[nameof(formModel.UserName)] = "��'� ����������� �� �������!";
                }
                if (String.IsNullOrEmpty(formModel.UserEmail))
                {
                    res[nameof(formModel.UserEmail)] = "���������� ������ �� ������!";
                }
                if (! _dataAccessor.UserDao.IsEmailFree(formModel.UserEmail))
                {
                    res[nameof(formModel.UserEmail)] = "������� ���������� ������ ��� ������������!";
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
