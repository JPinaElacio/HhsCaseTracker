using Microsoft.EntityFrameworkCore;
using HhsCaseTracker.Api.Models;

namespace HhsCaseTracker.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Case> Cases => Set<Case>();
    }
}
