using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.SliderDtos
{
    public record CreateSliderDto
    (
        IFormFile ImageUrls
        );
}
