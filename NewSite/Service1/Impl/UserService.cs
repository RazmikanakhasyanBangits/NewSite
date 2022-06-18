using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Helper_s.Impl;
using NewSite.Helper_s.Interface;
using NewSite.Models;
using NewSite.Models.Enums;
using NewSite.Repository.Abstraction;
using NewSite.Service.Interface;

namespace NewSite.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IConfiguration config;
        private readonly IHttpContextAccessor accessor;
        private readonly IUserDetailsRepository userDetailsRepository;

        public UserService(IUserRepository userRepository, IMapper mapper,
            ITokenService tokenService, IConfiguration config, IHttpContextAccessor accessor,
            IUserDetailsRepository userDetailsRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.config = config;
            this.accessor = accessor;
            this.userDetailsRepository = userDetailsRepository;
        }

        public async Task<bool> AddUserAsync(AddUserRequestModel model)
        {

            var user = mapper.Map<User>(model);

            accessor.HttpContext.Session.SetString("UserToReg", model.Email);
            user.RoleId = (short)UserRoles.User;


           return  userRepository.AddAsync(user).IsCompleted;
        }

        public async Task AddUserDetailsAsync(UserDetailsRequestModel model)
        {
            var userDetails = mapper.Map<UserDetails>(model);
            var email = accessor.HttpContext.Session.GetString("UserToReg").ToString();

            var user = await userRepository.Get(x => x.Email == email);
            userDetails.UserId = (long)user.Id;
            await userDetailsRepository.AddAsync(userDetails);
        }

        public void LogOut()
        {
            accessor.HttpContext.Session.Remove("Token");
        }

        public async Task<User> GetUserInfoAsync(GetUserRequestModel model)
        {
            string generateToken = null;
            var user = mapper.Map<User>(model);
            try
            {
                var userInfo = await userRepository.GetAsync(x => x.Email == user.Email && x.Password == user.Password,
                    includes: i => i.Include(y => y.Detail), false);
                if (userInfo != null)
                {
                    generateToken = tokenService.BuildToken(config["Jwt:Key"].ToString(), config["Jwt:Issuer"].ToString(), userInfo);
                    if (generateToken != null)
                    {
                        accessor.HttpContext.Session.SetString("Token", generateToken);
                    }
                    else
                    {
                        return null;
                    }
                }
                return userInfo;

            }
            catch (Exception)
            {

                return null;
            }



        }
    }
}
