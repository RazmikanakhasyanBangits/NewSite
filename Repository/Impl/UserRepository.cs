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
        private readonly IOldPasswordRepository passwordRepository;

        public UserRepository(IServiceScopeFactory scopeFactory, IAbstractCaching abstractCaching,
            NewSiteContext context, IOldPasswordRepository passwordRepository)
            : base(scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            this.abstractCaching = abstractCaching;
            _context = context;
            this.passwordRepository = passwordRepository;
        }

        public async Task ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            var user = await abstractCaching.GetAsync<User>(CachKeys.UserKey);
            var isUsedPassword = await passwordRepository.GetDetailsAsync(x => x.Password == model.NewPassword && x.UserId==user.Id)!=null;
            if (model.CurrentPassword == user.Password && model.NewPassword == model.RepeatNewPasword && isUsedPassword==false)
            {
                await passwordRepository.AddAsync(new OldPasswords
                {
                    CreationDate = DateTime.UtcNow.ToString("yyyy:mm:dd:hh:mm"),
                    Password = model.NewPassword,
                    UserId=user.Id,

                });

                user.Password = model.NewPassword;
                _context.Update(user);
                await _context.SaveChangesAsync();
                await abstractCaching.ClearAsync(CachKeys.UserKey);
                await abstractCaching.SetAsync(CachKeys.UserKey,user);
            }
        }
    }
}
