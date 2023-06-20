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

        public DbSet<Ad> Ads { get; set; }
    }
}
