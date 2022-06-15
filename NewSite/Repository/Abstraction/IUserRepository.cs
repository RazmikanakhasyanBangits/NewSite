using NewSite.Entity;

namespace NewSite.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetInfoAsync(User user);
    }
}
