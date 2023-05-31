using Microsoft.EntityFrameworkCore;
using SecretSharing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSharing.Infrastructure.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options): base(options) { }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<UserText> UserTexts { get; set; }

    }
}
