using Microsoft.AspNetCore.Identity;

namespace SecretSharing.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public List<UserFile> UserFiles { get; set; }
        public List<UserText> UserTexts { get; set; }
    }
}
