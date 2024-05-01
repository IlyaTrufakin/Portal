using Portal.Data.Context;
using Portal.Data.Entities;

namespace Portal.Data.Dal
{
    public class UserDao
    {
        private readonly DataContext _context;
        public UserDao(DataContext context)
        {
            _context = context;
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
