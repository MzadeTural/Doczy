using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.WorkPlace
{
    public record CreateHospitalDto(
      string? Name,
      IFormFile Icon
   );
}
