using System.ComponentModel.DataAnnotations;

namespace Ads.Web.Services.AdService.DTOs
{
    public class GetAdsRequestDto
    {
        [Range(minimum: 1, maximum: 9999)]
        public int Page { get; set; } = 1;
    }
}
