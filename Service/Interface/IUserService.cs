using Microsoft.AspNetCore.Http;
using Repository.Entity;
using Shared.Models;

namespace Service.Interface;

public interface IUserService 
{
    Task AddUserAsync(AddUserRequestModel model);
    Task AddUserDetailsAsync(IFormFile file,UserDetailsRequestModel model);
    Task ChangePasswordAsync(ChangePasswordRequestModel model);
    Task ForgotPassword(ForgotPasswordModel model);
    Task<User> GetInfo(GetUserRequestModel model);
    Task<User> GetUserInfoAsync(GetUserRequestModel user);
    void LogOut();
    Task<IEnumerable<UserSearchResultModel>> SearchUser(FindeUserModel user);
    Task SendVerificationCode(string email, string code);
    Task VerifyAccountAsync(VerifyAccountModel model);
}