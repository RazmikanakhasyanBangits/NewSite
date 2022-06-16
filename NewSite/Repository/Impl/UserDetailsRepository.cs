using NewSite.Entity;
using NewSite.Repository.Abstraction;

namespace NewSite.Repository.Impl
{
    public class UserDetailsRepository : GenericRepository<UserDetails>,IUserDetailsRepository
    {
        private readonly NewSiteContext context;
        public UserDetailsRepository(NewSiteContext context) : base(context)
        {
            this.context = context;
        }

    }
}
