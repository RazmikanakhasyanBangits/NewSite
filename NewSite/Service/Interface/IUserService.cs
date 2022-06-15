using NewSite.Entity;
using NewSite.Models;

namespace NewSite.Service.Interface
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserRequestModel user);
        Task<User> GetUserInfoAsync(GetUserRequestModel user);
    }
}
