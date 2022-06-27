
using Repository.Entity;

namespace Service.Interface
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, User user);
        Dictionary<string, string> GetToken();
        bool IsTokenValid(string key, string issuer, string token);
    }
}