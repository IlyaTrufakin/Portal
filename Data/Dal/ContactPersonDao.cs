using Portal.Data.Context;
using Portal.Data.Entities;

namespace Portal.Data.Dal
{
    public class ContactPersonDao
    {
        private readonly DataContext _context;
        private readonly Object _dbLocker;

        public ContactPersonDao(DataContext context, Object dbLocker)
        {
            _context = context; 
            _dbLocker = dbLocker;
        }

        public ContactPerson GetPersonProfileByUserId(user.Id);
    }
}
