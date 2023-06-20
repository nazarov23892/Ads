namespace Ads.Web.Services.AdService.DTOs
{
    public class GetAdsResponseDto
    {
        public int Page { get; set; }
        public IEnumerable<GetAdsResponseItemDto>? Items { get; set; }
    }

    public class GetAdsResponseItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CreatedUtc { get; set; } = string.Empty;
        public string? ImgUrl { get; set; }
    }
}
