using Repository.Entity;
using Repository.Interface;
using Shared.Models;

namespace Repository.Impl
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly NewSiteContext _context;

        public UserRepository(NewSiteContext context) : base(context)
        {
            _context = context;
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
