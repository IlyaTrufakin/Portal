using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.Home;
using System.Diagnostics;
using Portal.Data.Dal;
using Portal.Services.Hash;
using Portal.Services.Kdf;
using Portal.Models.Account.SignUp;
using System.Net;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHashService _hashService;
        private readonly IKdfService _kdfService;
        private readonly DataAccessor _dataAccessor;

        public HomeController(ILogger<HomeController> logger,
                                IHashService hashService,
                                IKdfService kdfService,
                                DataAccessor dataAccessor)
        {
            _logger = logger;
            _hashService = hashService;
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
                PageTitle = "Вхід",
            };
            return View(model);
        }



        public ViewResult SignUp(SignUpFormModel? formModel)
        {
            SignUpPageModel pageModel = new()
            {
                PageTitle = "Реєстрація",
                SignUpFormModel = formModel,
                ValidationErrors = _ValidateSignUpModel(formModel)
            };

            if (formModel?.UserAccountEmail != null)
            {
                if (pageModel.ValidationErrors.Any())
                {
                    pageModel.Message = "Реєстрація відхилена";
                    pageModel.IsSuccess = false;
                }
                else
                {
                    _dataAccessor.UserDao.SignUpUser(mapUser(formModel));
                    pageModel.Message = "Реєстрація успішна";
                    pageModel.IsSuccess = true;

                }

                formModel = null;
            }

            return View(pageModel);
        }

        private (Data.Entities.User, Data.Entities.ContactPerson) mapUser(SignUpFormModel formModel)
        {
            string salt = Guid.NewGuid().ToString();
            var profile = new Data.Entities.ContactPerson
            {
                Id = Guid.NewGuid(),
                Name = formModel.UserName,
                SurName = formModel.UserSurName,
                Phone = formModel.UserPhoneNumber,
                WorkEmail = formModel.UserWorkEmail,
                Description = formModel.UserDescription,
                IsChecked = formModel.IsChecked,
                IsVisible = formModel.IsVisible,
                AvatarUrl = formModel.SavedFilename
            };

            var user = new Data.Entities.User
            {
                Id = Guid.NewGuid(),
                ContactPersonId = profile.Id,
                AccountEmail = formModel.UserAccountEmail,
                Salt = salt,
                DerivedKey = _kdfService.GetDerivedKey(formModel.UserPassword, salt),
                UserRegistered = DateTime.Now
            };
            return (user, profile);
        }


        private Dictionary<String, String> _ValidateSignUpModel(SignUpFormModel? formModel)
        {
            Dictionary<String, String> res = new(); // перелік помилок по формі
            if (formModel == null)
            {
                res[nameof(formModel)] = "Model is null";
            }
            else
            {
                if (String.IsNullOrEmpty(formModel.UserName))
                {
                    res[nameof(formModel.UserName)] = "Ім'я користувача не вказано!";
                }
                if (String.IsNullOrEmpty(formModel.UserSurName))
                {
                    res[nameof(formModel.UserSurName)] = "Призвіще користувача не вказано!";
                }
                if (String.IsNullOrEmpty(formModel.UserPhoneNumber))
                {
                    res[nameof(formModel.UserPhoneNumber)] = "Номер телефону користувача не вказано!";
                }
                if (String.IsNullOrEmpty(formModel.UserAccountEmail))
                {
                    res[nameof(formModel.UserAccountEmail)] = "Електронна адреса не задана!";
                }
                if (!_dataAccessor.UserDao.IsEmailFree(formModel.UserAccountEmail))
                {
                    res[nameof(formModel.UserAccountEmail)] = "Вказана електронна адреса вже зареєстрована!";
                }
                if (String.IsNullOrEmpty(formModel.UserPassword))
                {
                    res[nameof(formModel.UserPassword)] = "Пароль не вказано!";
                }
                if (formModel.UserPassword != formModel.UserPasswordConfirm)
                {
                    res[nameof(formModel.UserPasswordConfirm)] = "Підтвердження паролю невірне!";
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
