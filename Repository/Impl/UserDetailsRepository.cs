using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        private readonly NewSiteContext context;
        public UserDetailsRepository(NewSiteContext context) : base(context)
{
            context.Database.EnsureCreated();
            this.context = context;
        }

    }
}
