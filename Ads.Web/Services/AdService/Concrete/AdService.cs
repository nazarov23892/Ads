using Ads.Web.DataContexts;
using Ads.Web.Services.AdService.DTOs;

namespace Ads.Web.Services.AdService.Concrete
{
    public class AdService : IAdService
    {
        private readonly AdDbContext _efDbContext;
        private const int PageSize = 10;

        public AdService(AdDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public GetAdsResponseDto GetAds(GetAdsRequestDto? adsRequestDto)
        {
            int pageStartsZero = adsRequestDto?.Page - 1 ?? 0;
            var query = _efDbContext.Ads
                .OrderBy(a => a.AdId)
                .Select(a => new GetAdsResponseItemDto
                {
                    Id = a.AdId,
                    Name = a.Name,
                    Price = a.Price,
                    CreatedUtc = a.DateCreatedUtc.ToString(),
                    ImgUrl = a.ImageUrls.FirstOrDefault(i => i.IsMain).Url
                })
                .Skip(count: pageStartsZero * PageSize)
                .Take(count: PageSize);

            return new GetAdsResponseDto
            {
                Page = 1 + pageStartsZero,
                Items = query
                    .ToList()
            };
        }
    }
}
