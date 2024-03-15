namespace Doczy.Business.DTOs.ServiceDtos
{
    public record CreateServiceDto(
    
    string Name,
    byte Duration,
    string Description,
    decimal Price,
    Guid ServiceTypeId
 );
}
