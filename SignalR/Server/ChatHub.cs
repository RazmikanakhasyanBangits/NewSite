using Microsoft.AspNetCore.SignalR;
using Repository.Interface;
using SignalRServer.Server.Interface;

namespace SignalR.Server
{
    public class ChatHub : Hub<IHubMethods>
    {
        public async Task SubscribeHub(string channelId,string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, channelId);

            await Clients.All.GetRequest(userName, userName);

        }
    }
}
