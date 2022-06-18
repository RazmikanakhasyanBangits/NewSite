using NewSite.Repository.Abstraction;
using Repository.Entity;

namespace NewSite.Repository.Impl
{
    public class UserRepository:IUserRepository
    {
        private readonly NewSiteContext _context;

        public UserRepository(NewSiteContext context)
        {
            _context = context;
        }

        public async Task<User> GetInfoAsync(User user)
        {
            var userData =   _context.Users.FirstOrDefault(x => x.UserName==user.UserName && x.Password==user.Password);
            if (userData == null) return null;

            return userData;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
