using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        private readonly IServiceScopeFactory scopeFactory;
        public UserDetailsRepository(IServiceScopeFactory scopeFactory)
            : base(scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

    }
}
