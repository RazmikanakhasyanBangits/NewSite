using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        private readonly NewSiteContext context;
        private readonly IServiceScopeFactory scopeFactory;
        public UserDetailsRepository(NewSiteContext context, IServiceScopeFactory scopeFactory) : base(context,scopeFactory)
        {
            this.context = context;
            this.scopeFactory = scopeFactory;
        }

    }
}
