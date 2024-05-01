using Portal.Data.Context;
using Portal.Data.Entities;
using Portal.Services.Kdf;

namespace Portal.Data.Dal
{
    public class UserDao
    {
        private readonly DataContext _context;
        private readonly IKdfService _kdfService;
        private readonly Object _dbLocker;
        public UserDao(DataContext context, IKdfService kdfService, object dbLocker)
        {
            _context = context;
            _kdfService = kdfService;
            _dbLocker = dbLocker;
        }

        public User? Authenticate(String email, String password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.AccountEmail == email);
            if (user != null && _kdfService.GetDerivedKey(password, user.Salt) == user.DerivedKey)
            {
                return user;
            }
            return null;
        }

        public User? GetUserById(String id)
        {
            User? user;
            lock (_dbLocker)
            {
                try { user = _context.Users.Find(Guid.Parse(id)); }
                catch { user = null; }
            }
            return user;
        }


        public bool IsEmailFree(String email)
        {
            return ! _context.Users.Where(u => u.AccountEmail == email).Any();
        }

        public void SignUpUser((User , ContactPerson) value)
        {
            _context.Users.Add(value.Item1);
            _context.ContactPersons.Add(value.Item2);
            _context.SaveChanges();
        }

    
    }
}
