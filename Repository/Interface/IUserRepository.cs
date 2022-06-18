using Repository.Entity;

namespace Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task AddUserAsync(User user);
        Task<User> GetInfoAsync(User user);
    }
}
