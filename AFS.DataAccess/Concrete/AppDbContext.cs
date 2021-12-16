using AFS.Entity.Concrete;
using AspNetCoreIdentityExample.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AFS.DataAccess.Models.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext) { }

        public DbSet<TranslationHistory> TranslationHistory { get; set; }
    }
}
