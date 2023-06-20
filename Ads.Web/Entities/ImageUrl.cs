namespace Ads.Web.Entities
{
    public class ImageUrl
    {
        public int ImageUrlId { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public int AdId { get; set; }
    }
}
