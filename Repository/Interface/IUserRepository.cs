using Repository.Entity;
using Shared.Models;

namespace Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task AddUserAsync(User user);
        Task ChangePasswordAsync(ChangePasswordRequestModel model);
        Task<User> GetInfoAsync(User user);
    }
}
