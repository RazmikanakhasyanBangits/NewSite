using NewSite.Entity;
using NewSite.Models;

namespace NewSite.Service.Impl.Profile
{
    public class UserDetailsProfile:AutoMapper.Profile
    {
        public UserDetailsProfile()
        {
            CreateMap<UserDetailsRequestModel, UserDetails>().ReverseMap();
        }
    }
}
