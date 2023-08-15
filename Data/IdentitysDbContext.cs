using EL.BlackList.API.IdentityAuth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Data
{
    public class IdentitysDbContext : IdentityDbContext<AppUser>
    {
        public IdentitysDbContext(DbContextOptions<IdentitysDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
