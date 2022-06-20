using Microsoft.Extensions.DependencyInjection;
using Repository.Entity;
using Repository.Interface;
using Shared.Models;

namespace Repository.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly NewSiteContext _context;
        private readonly IServiceScopeFactory scopeFactory;

        public UserRepository(NewSiteContext context, IServiceScopeFactory scopeFactory) : base(context,scopeFactory)
        {
            _context = context;
            this.scopeFactory = scopeFactory;
        }

        public async Task ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (model.CurrentPassword == user.Password && model.NewPassword == model.RepeatNewPasword)
            {
                user.Password = model.NewPassword;
                await _context.SaveChangesAsync();
            }
        }
    }
}
