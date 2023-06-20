using Ads.Web.Services.AdService.DTOs;

namespace Ads.Web.Services.AdService
{
    public interface IAdService
    {
        GetAdsResponseDto GetAds(GetAdsRequestDto? adsRequestDto);
        GetAdItemResponseDto? GetAdItem(int id);
        CreateAdResponseDto? CreateAd(CreateAdRequestDto createAdRequestDto);
        bool HasValidationProblems { get; }
        IDictionary<string, string[]> ValidationProblems { get; }
    }
}
