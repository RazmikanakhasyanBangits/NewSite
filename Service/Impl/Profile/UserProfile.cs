using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Helper_s;
using Shared.Models;

namespace Repository.Service.Impl.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, AddUserRequestModel>().ReverseMap()
                .AfterMap((x, y) => y.Password = y.Password.GetHash512());

            CreateMap<User, GetUserRequestModel>().ReverseMap()
              .AfterMap((x, y) => y.Password = y.Password.GetHash512());

            CreateMap<UserDetailsRequestModel, UserDetails>();

            CreateMap<ChangePasswordRequestModel, ChangePasswordRequestModel>()
                .AfterMap((x, y) => y.CurrentPassword = y.CurrentPassword.GetHash512())
                .AfterMap((x, y) => y.NewPassword = y.NewPassword.GetHash512())
                .AfterMap((x, y) => y.RepeatNewPasword = y.RepeatNewPasword.GetHash512());

            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<UserDetailsModel, UserDetails>().ReverseMap();
        }
    }
}
