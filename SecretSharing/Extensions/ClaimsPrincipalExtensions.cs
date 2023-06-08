using System.Security.Claims;

namespace SecretSharing.Extensions
{
    // Extension that help retrive data using claim
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveIdFromPrincipal(this ClaimsPrincipal User)
        {
            return User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
