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

    public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService,
        IConfiguration config, IHttpContextAccessor accessor, IUserDetailsRepository userDetailsRepository, IFileService fileService, 
        IEmailService emailService)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.tokenService = tokenService;
        this.config = config;
        this.accessor = accessor;
        this.userDetailsRepository = userDetailsRepository;
        this.fileService = fileService;
        this.emailService = emailService;
    }


    public async Task AddUserAsync(AddUserRequestModel model)
    {

        var user = mapper.Map<User>(model);

        accessor.HttpContext.Session.SetString("Email", model.Email);
        user.RoleId = (short)UserRoles.User;
        await userRepository.AddAsync(user);
    }

    public async Task AddUserDetailsAsync(IFormFile file,UserDetailsRequestModel model)
    {
        var userDetails = mapper.Map<UserDetails>(model);
        var email = accessor.HttpContext.Session.GetString("Email").ToString();
        var path = await fileService.SaveFile(file, email);


        var user = await userRepository.Get(x => x.Email == email);
        userDetails.UserId = (long)user.Id;
        userDetails.Photo = path;
        await userDetailsRepository.AddAsync(userDetails);
    }

    public void LogOut()
    {
        accessor.HttpContext.Session.Clear();
    }
    public async Task<User> GetUserInfoAsync(GetUserRequestModel model)
    {
        string generateToken = null;
        var user = mapper.Map<User>(model);
        var userInfo = await userRepository.GetAsync(x => x.Email==user.Email && x.Password==user.Password,
                              includes:i=>i.Include(x=>x.Details),false);
        var userDataToSet = mapper.Map<UserModel>(userInfo);
        if (userInfo != null)
        {
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

    public async Task ForgotPassword(ForgotPasswordModel model)
    {
         config.GetSection("EmailConfiguration").Get<EmailCredentialsModel>();
        var user = await userRepository.GetAsync(x => x.Email == model.Email,null,false);
        EmailConfigurationModel emailConfig = new()
        {
            From = new MailAddress(EmailCredentialsModel.From, "From Addres"),
            To = new MailAddress(user.Email, "To Addres"),
            Password = EmailCredentialsModel.Password,
            Body = "Test",
            Title="New Site"
        };
        emailService.SendCode(emailConfig);
    }


}
