using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NewSite.Repository.Abstraction;
using Repository.Entity;
using Service.Interface;
using Shared.Models;
using Shared.Models.Enums;

namespace NewSite.Service.Impl;

public class UserService:IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly ITokenService tokenService;
    private readonly IConfiguration config;
    private readonly IHttpContextAccessor accessor;

    public UserService(IUserRepository userRepository, IMapper mapper,
        ITokenService tokenService, IConfiguration config, IHttpContextAccessor accessor)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.tokenService = tokenService;
        this.config = config;
        this.accessor = accessor;
    }

    public async Task AddUserAsync(AddUserRequestModel model)
    {

        var user = mapper.Map<User>(model);
        user.RoleId = (short)UserRoles.User;
        await userRepository.AddUserAsync(user);
    }

    public async Task<User> GetUserInfoAsync(GetUserRequestModel model)
    {
        string generateToken = null;
        var user = mapper.Map<User>(model);
        var userInfo = await userRepository.GetInfoAsync(user);
        if (userInfo != null)
{
            generateToken = tokenService.BuildToken(config["Jwt:Key"].ToString(), config["Jwt:Issuer"].ToString(), userInfo);
            if (generateToken != null)
            {
                accessor.HttpContext.Session.SetString("Token", generateToken);
            }
            return userInfo;
        }
        else
        {
            return null;
        }

    }
}
