using AutoMapper;
using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Models;
using NewSite.Repository.Abstraction;
using NewSite.Service.Interface;

namespace NewSite.Service.Impl
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task AddUserAsync(AddUserRequestModel model)
        {

            var user = mapper.Map<User>(model);
            await userRepository.AddUserAsync(user);
        }

        public async Task<User> GetUserInfoAsync(User user)
        {
            return await userRepository.GetInfoAsync(user);
        }
    }
}
