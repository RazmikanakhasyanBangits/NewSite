using NewSite.Entity;
using NewSite.Models;

namespace NewSite.Service.Interface
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserRequestModel user);
        Task AddUserDetailsAsync(UserDetailsRequestModel model);
        Task<User> GetUserInfoAsync(GetUserRequestModel user);
        void LogOut();
    }
}
