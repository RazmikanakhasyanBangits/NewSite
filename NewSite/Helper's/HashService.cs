using System.Security.Cryptography;
using System.Text;

namespace NewSite.Helper_s
{
    public static class HashService
    {
        public static string GetHash512(this string Password)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashBytes = Encoding.UTF8.GetBytes(Password);
                var hash = sha512.ComputeHash(hashBytes);
                var hashString = string.Join("", hash.Select(_ => _.ToString("x2")));
                return hashString;
            }
        }
        public static string GetHash256(this string Password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = Encoding.UTF8.GetBytes(Password);
                var hash = sha256.ComputeHash(hashBytes);
                var hashString = string.Join("", hash.Select(_ => _.ToString("x2")));
                return hashString;
            }
        }
    }
}
