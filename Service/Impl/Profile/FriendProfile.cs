using Repository.Entity;

namespace Service.Impl.Profile
{
    public class FriendProfile : AutoMapper.Profile
    {
        public FriendProfile()
        {
            CreateMap<UserDetails, Friend>()
                .BeforeMap((x, y) => x.User = null)
                .BeforeMap((x, y) => x.Id = null);
        }
    }
}
