using Repository.Entity;

namespace SignalRServer.Server.Interface
{
    public interface IHubMethods
    {
        Task AddUserAsync(User model);
        Task GetRequest(string name,string message);
    }
}
