using NewSite.Entity;

namespace NewSite.Service.Interface
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserInfoAsync(User user);
    }
}
