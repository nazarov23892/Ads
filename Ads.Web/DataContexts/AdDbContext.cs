using Ads.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ads.Web.DataContexts
{
    public class AdDbContext : DbContext
    {
        public AdDbContext(DbContextOptions<AdDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Database.IsSqlite())
            {
                modelBuilder.Entity<Ad>().Property(a => a.Price).HasConversion<double>();
            }
        }

        public DbSet<Ad> Ads { get; set; }
    }
}
