using SecretSharing.Core.Entities.Identity;

namespace SecretSharing.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser);
    }
}