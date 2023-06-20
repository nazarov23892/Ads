namespace Ads.Web.Services.AdService.DTOs
{
    public class GetAdItemResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedUtc { get; set; } = string.Empty;
        public IEnumerable<string>? ImageUrls { get; set; }
    }
}
