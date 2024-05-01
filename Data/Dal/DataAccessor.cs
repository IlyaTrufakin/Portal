using Portal.Data.Context;
using Portal.Services.Kdf;

namespace Portal.Data.Dal
{
    //сервіс реєструємо в контейнере
    public class DataAccessor
    {
        private readonly DataContext _context;
        private readonly IKdfService _kdfService;
        private readonly Object _dbLocker = new Object();
        public UserDao UserDao { get; private set; }
        public ContactPersonDao ContactPersonDao { get; private set; }
        public DataAccessor(DataContext context, IKdfService kdfService)
        {
            _context = context;
            UserDao = new(_context, kdfService, _dbLocker);
            ContactPersonDao = new(_context, _dbLocker);
            _kdfService = kdfService;
        }
    }
}
