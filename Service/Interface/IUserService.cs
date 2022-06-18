using Microsoft.AspNetCore.Http;
using Repository.Entity;
using Shared.Models;

namespace Service.Interface;

public interface IUserService
{
    Task AddUserAsync(AddUserRequestModel model);
    Task AddUserDetailsAsync(IFormFile file,UserDetailsRequestModel model);
    Task<User> GetUserInfoAsync(GetUserRequestModel user);
    void LogOut();
}