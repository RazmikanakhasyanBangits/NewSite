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
    Task SendVerificationCode(string email, string code);
    Task Test1();
    Task VerifyAccountAsync(VerifyAccountModel model);
}