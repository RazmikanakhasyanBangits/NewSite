using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;

namespace Repository.Impl
{
    public class OldPasswordRepository : GenericRepository<OldPasswords>, IOldPasswordRepository
    {
        private readonly IServiceScopeFactory scopeFactory;
        public OldPasswordRepository(IServiceScopeFactory scopeFactory) : base(scopeFactory){}
    }
}
