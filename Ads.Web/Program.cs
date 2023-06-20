using Ads.Web.DataContexts;
using Ads.Web.Services.AdService.Concrete;
using Ads.Web.Services.AdService;
using Microsoft.EntityFrameworkCore;
using Ads.Web.Data;

namespace Ads.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AdDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "ad-db");
            });
            builder.Services.AddTransient<IAdService, AdService>();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var efDbCOntext = scope.ServiceProvider.GetRequiredService<AdDbContext>();
                SeedTool.SeedData(efDbCOntext);
            }

            app.MapGet("/", () => "Hello World!");
            app.MapGet(
               pattern: "/api/ads",
               handler: (IAdService adService) =>
               {
                   return adService.GetAds();
               });

            app.Run();
        }
    }
}