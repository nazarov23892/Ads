using Ads.Web.DataContexts;
using Ads.Web.Services.AdService.Concrete;
using Ads.Web.Services.AdService;
using Microsoft.EntityFrameworkCore;
using Ads.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Ads.Web.Services.AdService.DTOs;

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
               handler: ([FromBody] GetAdsRequestDto? getAdsRequest, IAdService adService) =>
               {
                   return adService.GetAds(getAdsRequest);
               });
            app.MapPost(
               pattern: "/api/ads",
               handler: ([FromBody] CreateAdRequestDto createRequest, IAdService adService) =>
               {
                   return adService.CreateAd(createRequest);
               });

            app.Run();
        }
    }
}