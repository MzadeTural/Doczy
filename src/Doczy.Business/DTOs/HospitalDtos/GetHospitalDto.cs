using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.HospitalDtos
{
    public record GetHospitalDto
   (
         string? Name,
      string IconUrl
        );
}
