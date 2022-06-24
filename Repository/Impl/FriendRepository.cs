using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class FriendRepository : GenericRepository<Friend>, IFriendRepository
    {
        private readonly IServiceScopeFactory serviceScope;

        public FriendRepository(IServiceScopeFactory serviceScope) : base(serviceScope)
        {
            this.serviceScope = serviceScope;
        }

    }
}
