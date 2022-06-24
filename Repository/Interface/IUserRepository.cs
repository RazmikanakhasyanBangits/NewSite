using Repository.Entity;
using Shared.Models;

namespace Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task ChangePasswordAsync(ChangePasswordRequestModel model);

    }
}
