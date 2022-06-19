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

        public async Task<User> GetInfoAsync(User user)
        {
            var userData = _context.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (userData == null) return null;

            return userData;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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
