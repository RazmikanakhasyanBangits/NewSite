using Repository.Entity;
using Shared.Models;

namespace Service.Interface;

public interface IUserService
{
    Task AddUserAsync(AddUserRequestModel user);
    Task<User> GetUserInfoAsync(GetUserRequestModel user);
}