using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Repository.Entity;
using Repository.Interface;
using Shared.Models;
using SignalRClient.Client.Interface;

namespace SignalRClient.Client.Impl
{
    public class Client : IClient
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        public Client(IUserRepository userRepository, IConfiguration configuration)
        {

            this.userRepository = userRepository;
            this.configuration = configuration;
        }


        public async Task Connect()
        {
            var connection = new HubConnectionBuilder()
                                       .WithUrl(new Uri(configuration["SignalR:Connection"]))
                                       .WithAutomaticReconnect()
                                       .Build();
            await connection.StartAsync();
            connection.On<User>("AddUserAsync", async (data) =>
             {
                
                 await userRepository.AddAsync(data);
             });

            await connection.InvokeAsync("SubscribeHub", "Group1", "FirstTry");
        }

    }
}
