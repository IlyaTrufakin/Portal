using Portal.Data.Context;

namespace Portal.Data.Dal
{
    //сервіс реєструємо в контейнере
    public class DataAccessor
    {
        private readonly DataContext _context;
        public UserDao UserDao { get; private set; }
        public DataAccessor(DataContext context)
        {
            _context = context;
            UserDao = new(_context);
        }
    }
}
