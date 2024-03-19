namespace Doczy.Business.DTOs.ServiceDtos
{
    public record GetServiceByTypeDto
   (
     string? Name,
     byte Duration,
     decimal Price
        );
}
