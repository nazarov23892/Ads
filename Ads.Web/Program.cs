using Ads.Web.DataContexts;
using Microsoft.EntityFrameworkCore;

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

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}