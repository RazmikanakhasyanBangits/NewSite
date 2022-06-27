using Microsoft.AspNetCore.SignalR;
using Repository.Entity;
using Repository.Interface;
using SignalR.Server.Interface;
using SignalRServer.Server.Interface;

namespace SignalR.Server.Impl
{
    public class Handler : IHandler
    {
        private readonly IHubContext<ChatHub,IHubMethods> HubContext;

        public Handler(IHubContext<ChatHub,IHubMethods> HubContext)
        {
            this.HubContext = HubContext;
        }

        public async Task<string> SearchUser(string userName)
        {
            await HubContext.Clients.All.GetUsersAsync(userName);
            return userName;
        }
    }
}
