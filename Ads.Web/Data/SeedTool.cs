using Ads.Web.DataContexts;
using Ads.Web.Entities;

namespace Ads.Web.Data
{
    public class SeedTool
    {
        public static void SeedData(AdDbContext efDbContext)
        {
            if (efDbContext.Ads.Any())
            {
                return;
            }
            for (int i = 0; i < 64; i++)
            {
                var ad = new Ad
                {
                    Name = $"ad-{1 + i}",
                    Description = $"ad description - {1 + i}",
                    DateCreatedUtc = DateTime.UtcNow,
                    Price = 100M + (i / 100M)
                };
                efDbContext.Ads.Add(ad);
            }
            efDbContext.SaveChanges();
            Console.WriteLine("> ef db context: seed data done");
        }
    }
}
