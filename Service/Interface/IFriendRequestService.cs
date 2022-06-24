using Repository.Entity;
using Shared.Models;

namespace Service.Interface
{
    public interface IFriendRequestService
    {
        Task<ErrorModel> AcceptFriendRequest(AddFriendRequestModel model);
        Task<ErrorModel> RejectFriendRequest(AddFriendRequestModel model);
        Task<ErrorModel> SendFriendRequestAsync(string email);
    }
}
