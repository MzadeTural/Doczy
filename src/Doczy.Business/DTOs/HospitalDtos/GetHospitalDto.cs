using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.HospitalDtos
{
    public record GetHospitalDto
   (
        Guid id,
         string? Name,
         string IconUrl
        );
}
