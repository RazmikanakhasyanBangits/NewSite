using Repository.Entity;

namespace SignalRServer.Server.Interface
{
    public interface IHubMethods
    {
        Task GetUsersAsync(string userName);
        Task SendMessage(string name,string message);
    }
}
