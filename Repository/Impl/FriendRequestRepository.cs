using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class FriendRequestRepository : GenericRepository<FriendRequests>,IFriendRequestRepository
    {
        private readonly IServiceScopeFactory serviceScope;

        public FriendRequestRepository(IServiceScopeFactory serviceScope) : base(serviceScope)
        {
            this.serviceScope = serviceScope;
        }
    }
}
