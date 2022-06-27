using Helper_s;
using Repository.Entity;
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

            CreateMap<User, UserSearchResultModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Details.Surname))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(x => x.Details.Photo))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(x => x.Details.Age)).ReverseMap();
        }
    }
}
