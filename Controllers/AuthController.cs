using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Data.Dal;

namespace Portal.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataAccessor _dataAccessor;

        public AuthController(DataAccessor dataAccessor)
        {
            _dataAccessor = dataAccessor;
        }

        [HttpGet]
        public object Get(String email, String password)
        {
            var user = _dataAccessor.UserDao.Authenticate(email, password);
            String status;
            if (user == null)
            {
                status = "error";
            }
            else
            {
                status = "success";
                //зберігаємо у сесію дані щодо автентифікації
                HttpContext.Session.SetString("auth-user-id", user.Id.ToString());
            }

            return new { status};
        }
    }
}

/*Контроллери: API ta MVC
MVC
різні дії запускаються різними адресами (/home/index) (/home/Privacy)
дії повертають представленя або IActionResult



API
різні дії запускаються однією адресою але різними методами запиту  (GET api/auth) (POST api/auth)
дії повертають об'єкти, які ASP перетворює на JSON


HTTP сессії - засоби для збереження даних між різними запитами
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0
після підключення сесії доступні через httpcontext скрізь
*/