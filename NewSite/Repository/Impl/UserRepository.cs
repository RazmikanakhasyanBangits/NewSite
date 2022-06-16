using NewSite.Entity;
using NewSite.Repository.Abstraction;

namespace NewSite.Repository.Impl
{
    public class UserRepository: GenericRepository<User>,IUserRepository
    {
        private readonly NewSiteContext _context;

        public UserRepository(NewSiteContext context):base(context)
        {
            _context = context;
        }

    }
}
