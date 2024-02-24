namespace Doczy.Business.DTOs.ServiceDtos
{
    public record CreateServiceDto(
        Guid DoctorId,
    string Name,
    byte Duration,
    string Description,
    double Price,
    Guid ServiceTypeId
 );
}
