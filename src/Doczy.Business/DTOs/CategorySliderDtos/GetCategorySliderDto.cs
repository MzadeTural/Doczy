using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.CategorySliderDtos
{
    public class GetCategorySliderDto
    {
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
