using Helper_s;
using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;
using Shared.Models;
using Shared.Models.Enums;

namespace Repository.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly NewSiteContext _context;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IAbstractCaching abstractCaching;

        public UserRepository(IServiceScopeFactory scopeFactory, IAbstractCaching abstractCaching, NewSiteContext context)
            : base(scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            this.abstractCaching = abstractCaching;
            _context = context;
        }

        public async Task ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            var user = await abstractCaching.GetAsync<User>(CachKeys.UserKey);
            if (model.CurrentPassword == user.Password && model.NewPassword == model.RepeatNewPasword)
            {
                user.Password = model.NewPassword;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
