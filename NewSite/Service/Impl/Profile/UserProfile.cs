using Microsoft.AspNetCore.Mvc;
using NewSite.Entity;
using NewSite.Helper_s;
using NewSite.Models;

namespace NewSite.Service.Impl.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, AddUserRequestModel>().ReverseMap()
                .AfterMap((x, y) => y.Password = y.Password.GetHash512());
        }
    }
}
