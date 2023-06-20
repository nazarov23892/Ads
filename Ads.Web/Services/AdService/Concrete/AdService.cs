using Ads.Web.DataContexts;
using Ads.Web.Services.AdService.DTOs;

namespace Ads.Web.Services.AdService.Concrete
{
    public class AdService : IAdService
    {
        private readonly AdDbContext _efDbContext;

        public AdService(AdDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public GetAdsResponseDto GetAds()
        {
            var query = _efDbContext.Ads
                .Select(a => new GetAdsResponseItemDto
                {
                    Id = a.AdId,
                    Name = a.Name,
                    Price = a.Price,
                    CreatedUtc = a.DateCreatedUtc.ToString()
                });

            return new GetAdsResponseDto
            {
                Page = 1,
                Items = query
                    .ToList()
            };
        }
    }
}
