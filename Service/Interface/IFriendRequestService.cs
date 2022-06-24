using Repository.Entity;
using Shared.Models;

namespace Service.Interface
{
    public interface IFriendRequestService
    {
        Task<ErrorModel> AcceptFriendRequest(AddFriendRequestModel model);
        Task RejectFriendRequest(long id);
        Task SendFriendRequestAsync(string email);
    }
}
