using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.ServiceTypeDtos
{
    public record CreateServiceTypeDto(
     string? Name,
     IFormFile Icon
  );
}
