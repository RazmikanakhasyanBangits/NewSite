using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Repository.Abstraction;
using NewSite.Service.Interface;

namespace NewSite.Service.Impl
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUserAsync(User user)
        {
           user.Password = user.Password.GetHash512();
            await userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserInfoAsync(User user)
        {
            return await userRepository.GetInfoAsync(user);
        }
    }
}
