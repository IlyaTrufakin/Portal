using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Models.Account;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
