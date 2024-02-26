using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.WorkPlace
{
    public record CreateWorkPlaceDto(
      string? Name,
      IFormFile Icon
   );
}
