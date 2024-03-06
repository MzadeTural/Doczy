namespace Doczy.Business.DTOs.DoctorDtos
{
    public record GetDoctorFilterDto
    (
        Guid? CategoryId,
        Guid? ServiceTypeId,
        double? MinPrice,
        double? MaxPrice,
        Guid workPlaceId
        );
}
