namespace Doczy.Business.DTOs.DoctorDtos
{
    public record GetDoctorFilterDto
    (
        Guid Id,
        Guid? CategoryId,
        Guid? ServiceTypeId,
        double? MinPrice,
        double? MaxPrice,
        Guid HospitalId
        );
}
