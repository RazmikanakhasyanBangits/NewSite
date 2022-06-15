using NewSite.Entity;

namespace NewSite.Helper_s.Interface
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, User user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
