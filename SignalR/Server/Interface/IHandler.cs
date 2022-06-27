using Repository.Entity;

namespace SignalR.Server.Interface
{
    public interface IHandler
    {
        Task<string> SearchUser(string userName);
    }
}
