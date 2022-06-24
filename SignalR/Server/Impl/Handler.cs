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

        public async Task<User> SendMessageAsync(string message)
        {
            var user = new User()
            {
                Email = "some@mail.com",
                Password = "111111",
                UserName = "username",
                RoleId=2,
                StatusId=4,
                VerificationCode="2547"
            };
            await HubContext.Clients.All.AddUserAsync(user);
            return await Task.FromResult(user);
        }
    }
}
