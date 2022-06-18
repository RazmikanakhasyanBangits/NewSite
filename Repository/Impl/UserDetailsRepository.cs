using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        private readonly NewSiteContext context;
        public UserDetailsRepository(NewSiteContext context) : base(context)
        {
            this.context = context;
        }

    }
}
