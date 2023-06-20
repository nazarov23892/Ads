using System.ComponentModel.DataAnnotations;

namespace Ads.Web.Services.AdService.DTOs
{
    public class CreateAdRequestDto
    {
        [MaxLength(200)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> ImageUrls { get; set; } = Enumerable.Empty<string>();

    }
}
