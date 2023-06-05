using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretSharing.Core.Entities.Identity;
using System.Security.Claims;

namespace SecretSharing.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> FindByEmailFromClaimPrincipal(
            this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
