namespace Doczy.Business.DTOs.Experiance
{
    public record UpdateExperianceDto
   (
       string Title,
       Guid HospitalId,
       string Location,
       bool currentlyWorking,
       DateTime StartDate,
       DateTime EndDate
       );
}
