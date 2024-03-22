using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.HospitalDtos
{
   
    public record UpdateHospitalDto(
     string? Name,
     IFormFile Icon
  );
}
