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
                    DateCreatedUtc = GetRandomDate(),
                    Price = GetRandomPrice(),
                    ImageUrls = new List<ImageUrl>()
                };
                var rnd = new Random();
                int num = rnd.Next(0, 4);
                for (int k = 0; k < num; k++)
                {
                    string img = $"ad-{1 + i}--image-{1 + k}.jpeg";
                    ad.ImageUrls.Add(new ImageUrl
                    {
                        Url = img,
                        IsMain = k == 0
                    });
                }
                efDbContext.Ads.Add(ad);
            }
            efDbContext.SaveChanges();
            Console.WriteLine("> ef db context: seed data done");
        }

        private static DateTime GetRandomDate()
        {
            Random rnd = new Random();
            return new DateTime(
                year: 2020 + rnd.Next(0, 4), 
                month: rnd.Next(1, 12), 
                day: rnd.Next(1, 28),
                hour: rnd.Next(1, 24),
                minute: rnd.Next(0, 60),
                second: rnd.Next(0, 60));
        }

        private static decimal GetRandomPrice()
        {
            Random rnd = new Random();
            return rnd.Next(10, 100) + (rnd.Next(1, 10) / 10M);
        }
    }
}
