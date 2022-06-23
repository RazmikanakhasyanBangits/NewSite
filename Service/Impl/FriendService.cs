using AutoMapper;
using Repository.Interface;
using Service.Interface;

namespace Service.Impl
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository friendRepository;
        private readonly IMapper mapper;
        public FriendService(IFriendRepository friendRepository, IMapper mapper)
        {
            this.friendRepository = friendRepository;
            this.mapper = mapper;
        }

        public async Task SendRequestAsync()
        {

        }
    }
}
