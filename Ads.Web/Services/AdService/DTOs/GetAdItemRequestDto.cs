using System.ComponentModel.DataAnnotations;

namespace Ads.Web.Services.AdService.DTOs
{
    public class GetAdItemRequestDto
    {
        [MaxLength(2)]
        public IEnumerable<string> Fields { get; set; } = Enumerable.Empty<string>();

    }
}
