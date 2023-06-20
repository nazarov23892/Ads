using Ads.Web.DataContexts;
using Ads.Web.Entities;
using Ads.Web.Services.AdService.Concrete.Queries;
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
                .SortAdBy(
                    propertyName: adsRequestDto?.SortName,
                    asc: adsRequestDto?.SortAsc ?? true)
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

        public CreateAdResponseDto? CreateAd(CreateAdRequestDto createAdRequestDto)
        {
            Ad newAd = new Ad
            {
                DateCreatedUtc = DateTime.UtcNow,
                Name = createAdRequestDto.Name ?? string.Empty,
                Description = createAdRequestDto.Description,
                Price = createAdRequestDto.Price,
                ImageUrls = createAdRequestDto.ImageUrls
                    .Select((s, i) => new ImageUrl
                    {
                        Url = s,
                        IsMain = i == 0
                    })
                    .ToList()
                };
            _efDbContext.Ads.Add(newAd);
            _efDbContext.SaveChanges();
            return new CreateAdResponseDto { Id = newAd.AdId };
        }
    }
}
