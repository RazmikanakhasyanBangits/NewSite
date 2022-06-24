using Repository.Entity;

namespace SignalR.Server.Interface
{
    public interface IHandler
    {
        Task<User> SendMessageAsync(string message);
    }
}
