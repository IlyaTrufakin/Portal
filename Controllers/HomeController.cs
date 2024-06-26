using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.Home;
using System.Diagnostics;
using Portal.Data.Dal;
using Portal.Services.Hash;
using Portal.Services.Kdf;
using Portal.Models.Account.SignUp;
using System.Net;
using Portal.Services.Upload;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHashService _hashService;
        private readonly IKdfService _kdfService;
        private readonly DataAccessor _dataAccessor;
        private readonly IUploadService _uploadService;

        public HomeController(ILogger<HomeController> logger,
                                IHashService hashService,
                                IKdfService kdfService,
                                DataAccessor dataAccessor,
                                IUploadService uploadService)
        {
            _logger = logger;
            _hashService = hashService;
            _kdfService = kdfService;
            _dataAccessor = dataAccessor;
            _uploadService = uploadService;
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

            if (formModel?.UserAccountEmail != null)
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
                if (String.IsNullOrEmpty(formModel.UserSurName))
                {
                    res[nameof(formModel.UserSurName)] = "������� ����������� �� �������!";
                }
                if (String.IsNullOrEmpty(formModel.UserPhoneNumber))
                {
                    res[nameof(formModel.UserPhoneNumber)] = "����� �������� ����������� �� �������!";
                }
                if (String.IsNullOrEmpty(formModel.UserAccountEmail))
                {
                    res[nameof(formModel.UserAccountEmail)] = "���������� ������ �� ������!";
                }
                if (!_dataAccessor.UserDao.IsEmailFree(formModel.UserAccountEmail))
                {
                    res[nameof(formModel.UserAccountEmail)] = "������� ���������� ������ ��� ������������!";
                }
                if (String.IsNullOrEmpty(formModel.UserPassword))
                {
                    res[nameof(formModel.UserPassword)] = "������ �� �������!";
                }
                if (formModel.UserPassword != formModel.UserPasswordConfirm)
                {
                    res[nameof(formModel.UserPasswordConfirm)] = "ϳ����������� ������ ������!";
                }
                // ���������� � �������
                if (!formModel.PrivacyConfirm)
                {
                    res[nameof(formModel.PrivacyConfirm)] = "Confirm expected";
                }
                //���� ���� ������� - �� ���������� ���� ��������
                if (res.Count == 0)
                {
                    if (formModel.AvatarFile != null)
                    {
                        try
                        {
                            formModel.SavedFilename = _uploadService.SaveFromFile(formModel.AvatarFile, "wwwroot/img/avatars/");
                        }
                        catch (Exception ex)
                        {
                            res[nameof(formModel.AvatarFile)] = ex.Message;
                        }
                    }
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
