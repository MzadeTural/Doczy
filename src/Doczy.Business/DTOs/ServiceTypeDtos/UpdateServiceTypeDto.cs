using Microsoft.AspNetCore.Http;

namespace Doczy.Business.DTOs.ServiceTypeDtos
{

    public record UpdateServiceTypeDto(
    string? Name,
    IFormFile Icon
 );
}
