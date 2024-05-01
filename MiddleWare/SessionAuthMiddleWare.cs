using System.Globalization;
using System.Security.Claims;
using Portal.Data.Dal;

namespace Portal.MiddleWare
{
    //класс з функціональністю
    public class SessionAuthMiddleWare
    {
        //при запуску проєкту вибудовується послідовність запуску middleware
        //і кожен клас одержує посилання на наступний
        private readonly RequestDelegate _next;

        public SessionAuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        //оскільки конструктор зайнятий під налаштування ланцюга викликів, інжекція здійснюється у
        //метод InvokeAsync - його параметри це ті ж сервіси, що й зазвичай у конструкторі інших класів
        public async Task InvokeAsync(HttpContext context, DataAccessor dataAccessor)
        {
            var userId = context.Session.GetString("auth-user-id");
            if (userId != null) // у сесії є дані про автентифікації
            {
                //перевіряємо ці дані
                var user = dataAccessor.UserDao.GetUserById(userId);
                if (user != null)
                {
                    //використовуємо вбудовану до ASP схему работи з даними автентифікації - твердженя Claims
                    //пари ключ-значення для позначения типових даних


  /*                  var claims = new List<Claim>();

                    // Утверждения на основе данных из объекта пользователя
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));

                    // Предположим, у пользователя есть профиль с дополнительной информацией
                    var profile = _dataAccessor.ProfileDao.GetProfileByUserId(user.Id);
                    if (profile != null)
                    {
                        // Утверждения на основе данных из профиля пользователя
                        claims.Add(new Claim("profile_city", profile.City));
                        claims.Add(new Claim("profile_country", profile.Country));
                    }

                    // Предположим, у пользователя есть роли
                    var roles = _dataAccessor.RoleDao.GetRolesByUserId(user.Id);
                    foreach (var role in roles)
                    {
                        // Утверждения на основе ролей пользователя
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    // Создание объекта ClaimsIdentity с утверждениями
                    var claimsIdentity = new ClaimsIdentity(claims, "custom_authentication");

                    // Создание объекта ClaimsPrincipal с ClaimsIdentity
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
*/









                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        new Claim(ClaimTypes.UserData, user.AvatarUrl ?? ""),
                        new Claim(ClaimTypes.DateOfBirth, user.Birthdate?.ToString() ?? "")
                    };
                    //context.User - ASP поле, яке збирає дані Claims та схеми автентифікації
                    context.User = new ClaimsPrincipal(
                        new ClaimsIdentity(claims, nameof(SessionAuthMiddleWare)) // назва схеми автентифікації
                        );
                }
            }
            //у процессі работи middleware має прийняти рішення чи продовжувати подальшу обробку
            //запиту, якщо так - то має бути викликаний наступний обробник
            await _next(context);
        }
    }

    //класс розширення - для створеня короткого методу app.UseSessionAuth()
    public static class SessionAuthMiddleWareExtention
    {
        public static IApplicationBuilder UseSessionAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionAuthMiddleWare>();
        }

    }

}