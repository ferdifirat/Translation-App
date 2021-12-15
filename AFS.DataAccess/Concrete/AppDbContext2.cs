using AFS.Entity.Concrete;
using AspNetCoreIdentityExample.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AFS.DataAccess.Models.Context
{
    public class AppDbContext2 : IdentityDbContext<User>
    {
        public AppDbContext2(DbContextOptions<AppDbContext2> dbContext) : base(dbContext) { }

        public DbSet<TranslationHistory> TranslationHistory { get; set; }
    }
}
