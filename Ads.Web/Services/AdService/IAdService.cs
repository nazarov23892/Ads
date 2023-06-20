using Ads.Web.Services.AdService.DTOs;

namespace Ads.Web.Services.AdService
{
    public interface IAdService
    {
        GetAdsResponseDto GetAds(GetAdsRequestDto? adsRequestDto);
    }
}
