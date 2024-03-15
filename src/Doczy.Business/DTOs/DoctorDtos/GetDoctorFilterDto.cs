namespace Doczy.Business.DTOs.DoctorDtos
{
    public record GetDoctorFilterDto
    (
       
        Guid? CategoryId,
        Guid? ServiceTypeId,
        string FullName,
        decimal? MinPrice,
        decimal? MaxPrice,
        Guid? HospitalId
        );
}
