namespace Doczy.Business.DTOs.DoctorDtos
{
    public record GetDoctorFilterDto
    (
       
        Guid? CategoryId,
        Guid? ServiceTypeId,
        string FullName,
        double? MinPrice,
        double? MaxPrice,
        Guid? HospitalId
        );
}
