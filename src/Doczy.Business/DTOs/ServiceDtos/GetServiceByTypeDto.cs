namespace Doczy.Business.DTOs.ServiceDtos
{
    public record GetServiceByTypeDto
   (
        Guid Id,
     string? Name,
     byte Duration,
     decimal Price
        );
}
