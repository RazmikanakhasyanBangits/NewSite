using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Entity;
using Repository.Interface;
using Service.Helper_s;
using Service.Interface;
using Shared.Models;
using Shared.Models.Enums;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Repository.Service.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly ITokenService tokenService;
    private readonly IConfiguration config;
    private readonly IHttpContextAccessor accessor;
    private readonly IUserDetailsRepository userDetailsRepository;
    private readonly IFileService fileService;
    private readonly IEmailService emailService;
    private readonly NewSiteContext context;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService,
        IConfiguration config, IHttpContextAccessor accessor, IUserDetailsRepository userDetailsRepository, IFileService fileService,
        IEmailService emailService, NewSiteContext context, IServiceScopeFactory serviceScopeFactory)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.tokenService = tokenService;
        this.config = config;
        this.accessor = accessor;
        this.userDetailsRepository = userDetailsRepository;
        this.fileService = fileService;
        this.emailService = emailService;
        this.context = context;
        this.serviceScopeFactory = serviceScopeFactory;
    }


    public async Task AddUserAsync(AddUserRequestModel model)
    {

        var user = mapper.Map<User>(model);

        accessor.HttpContext.Session.SetString("Email", model.Email);
        user.RoleId = (short)UserRoles.User;
        user.StatusId = 4;
        user.VerificationCode = Random.Shared.Next(10000, 99999).ToString();
        await userRepository.AddAsync(user);
        await SendVerificationCode(user.Email, user.VerificationCode);
    }

    public async Task VerifyAccountAsync(VerifyAccountModel model)
    {
        var user = await userRepository.GetAsync(x => x.Email == model.Email, null, false);
        if (user != null && user.VerificationCode == model.VerificationCode)
        {
            user.StatusId = 2;
            await userRepository.UpdateAsync(user);
        }
    }
    public async Task AddUserDetailsAsync(IFormFile file, UserDetailsRequestModel model)
    {
        var userDetails = mapper.Map<UserDetails>(model);
        var email = accessor.HttpContext.Session.GetString("Email").ToString();
        var path = await fileService.SaveFile(file, email);


        var user = await userRepository.Get(x => x.Email == email);
        userDetails.UserId = (long)user.Id;
        userDetails.Photo = path;
        await userDetailsRepository.AddAsync(userDetails);
    }

    public async void LogOut()
    {

        var email = accessor.HttpContext?.GetClaimValueFromToken(ClaimTypes.Email);
        var user = await userRepository.GetAsync(x => x.Email == email, null, false);
        user.StatusId = 2;
        await userRepository.UpdateAsync(user);
        if (accessor.HttpContext !=null)
        {
        accessor.HttpContext.Session.Remove("Token");
        }
    }
    public async Task<User> GetUserInfoAsync(GetUserRequestModel model)
    {
        string generateToken = null;
        var user = mapper.Map<User>(model);
        var userInfo = await userRepository.GetAsync(x => x.Email == user.Email && x.Password == user.Password &&
                                                             x.StatusId != 4 && x.StatusId != 3,
                                                             includes: i => i.Include(x => x.Details), false);
        var userDataToSet = mapper.Map<UserModel>(userInfo);
        if (userInfo != null)
        {
            userInfo.StatusId = 1;
            await userRepository.UpdateAsync(userInfo);

            generateToken = tokenService.BuildToken(config["Jwt:Key"].ToString(), config["Jwt:Issuer"].ToString(), userInfo);
            if (generateToken != null)
            {
                accessor.HttpContext.Session.SetString("Token", generateToken);
                accessor.HttpContext.Session.SetString("User", JsonSerializer.Serialize(userDataToSet));
            }
            return userInfo;
        }
        else
        {
            return null;
        }
    }

    public async Task ChangePasswordAsync(ChangePasswordRequestModel model)
    {
        model.Email = accessor.HttpContext?.GetClaimValueFromToken(ClaimTypes.Email);
        var user = mapper.Map<ChangePasswordRequestModel>(model);
        await userRepository.ChangePasswordAsync(user);
    }

    public async Task SendVerificationCode(string email, string code)
    {
        config.GetSection("EmailConfiguration").Get<EmailCredentialsModel>();
        EmailConfigurationModel emailConfig = new()
        {
            From = new MailAddress(EmailCredentialsModel.From, "From Addres"),
            To = new MailAddress(email, "To Addres"),
            Password = EmailCredentialsModel.Password,
            Body = $"<font size='21'>Your Verification Code is <i>{code}</font></i><br>" +
            $"<img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFWnD7klSoUtZR4LNtlboGX8kk6CBR6QvGFg&usqp=CAU'>",
            Title = "New Site"
        };

        await emailService.SendCode(emailConfig);
    }
    public async Task ForgotPassword(ForgotPasswordModel model)
    {
        config.GetSection("EmailConfiguration").Get<EmailCredentialsModel>();
        var verificationCode = Random.Shared.Next(10000, 99999);
        var user = await userRepository.GetAsync(x => x.Email == model.Email, null, false);
        if (user!=null)
        {
            user.VerificationCode = verificationCode.ToString();
            await userRepository.UpdateAsync(user);
            EmailConfigurationModel emailConfig = new()
            {
                From = new MailAddress(EmailCredentialsModel.From, "From Addres"),
                To = new MailAddress(user.Email, "To Addres"),
                Password = EmailCredentialsModel.Password,
                Body = $"<font size='21'>Your Verification Code is <i>{verificationCode}</font></i><br>" +
                $"<img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRFWnD7klSoUtZR4LNtlboGX8kk6CBR6QvGFg&usqp=CAU'>",
                Title = "New Site"
            };
            await emailService.SendCode(emailConfig);
        }
        
    }


}
