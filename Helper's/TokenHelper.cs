using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Helper_s;

public static class TokenHelper
{
    public static string GetClaimValueFromToken(this HttpContext context, string claim)
    {
        return context?.User?.FindFirst(claim)?.Value;
    }

    public static string GetClaimValueFromClaimPrincipal(string claim)
    {
        return (ClaimsPrincipal.Current?.Identities?.First()?.Claims?.ToList())?.FirstOrDefault((x) => x.Type.Equals(claim, StringComparison.OrdinalIgnoreCase))?.Value;
    }

    public static string GetClaimValueFromHubContext(this HubCallerContext context, string claim)
    {
        return ((ClaimsIdentity)(context?.User?.Identity))?.FindFirst(claim)?.Value;
    }
}
