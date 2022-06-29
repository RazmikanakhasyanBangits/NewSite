using AutoMapper;
using Helper_s;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Interface;
using Service.Interface;
using Shared.Models;
using Shared.Models.Enums;

namespace Service.Impl
{
    public class FriendRequestService : IFriendRequestService
    {
        #region impotrs
        private readonly IFriendRequestRepository friendRequestRepository;
        private readonly IUserRepository userRepository;
        private readonly IAbstractCaching abstractCaching;
        private readonly IFriendRepository friendRepository;
        private readonly IMapper mapper;
        #endregion
        public FriendRequestService(IFriendRequestRepository friendRequestRepository, IUserRepository userRepository,
                                    IAbstractCaching abstractCaching, IMapper mapper, IFriendRepository friendRepository)
        {
            this.friendRequestRepository = friendRequestRepository;
            this.userRepository = userRepository;
            this.abstractCaching = abstractCaching;
            this.mapper = mapper;
            this.friendRepository = friendRepository;
        }

        public async Task SendFriendRequestAsync(int userId)
        {
            var currentUser = await abstractCaching.GetAsync<User>(CachKeys.UserKey);
            var sendRequestTo = await userRepository.GetAsync(x => x.Id == userId);
            var request = new FriendRequests()
            {
                FromId = currentUser.Id,
                UserId = sendRequestTo.Id
            };
            await friendRequestRepository.AddAsync(request);
        }

        public async Task RejectFriendRequest(long id)
        {
            await friendRequestRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<ErrorModel> AcceptFriendRequest(AddFriendRequestModel model)
        {
            var currentUser = await abstractCaching.GetAsync<User>(CachKeys.UserKey);
            var acceptRequestFrom = await userRepository.GetAsync(x => x.Id == model.FromId, includes: i => i.Include(x => x.Details));


            var currentUserFriend = mapper.Map<Friend>(acceptRequestFrom.Details);
            var senderUserFriend = mapper.Map<Friend>(currentUser.Details);

            currentUserFriend.UserId = currentUser.Id;
            senderUserFriend.UserId = acceptRequestFrom.Id;

            try
            {
                await friendRequestRepository.DeleteAsync(x => x.Id == model.Id);
                await friendRepository.AddAsync(currentUserFriend);
                await friendRepository.AddAsync(senderUserFriend);
                return new ErrorModel()
                {
                    Status = 200,
                    Description = "Success",
                };
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("inner"))
                {
                    return new ErrorModel()
                    {
                        Status = 201,
                        Description = "Allready Friends",
                    };
                }
                else
                {
                    return new ErrorModel()
                    {
                        Status = 500,
                        Description = "Request Does Not Exist",
                    };
                }
               
            }

        }
    }
}
