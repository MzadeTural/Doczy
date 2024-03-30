using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.CategorySliderDtos
{
    public class CreateCategorySliderDto
    {
        public string Title { get; set; }
        public IFormFile IconUrl { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
