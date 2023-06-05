using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecretSharing.Core.Entities;
using SecretSharing.Core.Entities.Identity;

namespace SecretSharing.Infrastructure.Data
{
    public class StoreContext : IdentityDbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<UserText> UserTexts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

        }

    }
}
