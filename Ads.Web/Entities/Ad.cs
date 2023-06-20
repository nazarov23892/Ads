namespace Ads.Web.Entities
{
    public class Ad
    {
        public int AdId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreatedUtc { get; set; }
        public ICollection<ImageUrl>? ImageUrls { get; set; }
    }
}
