namespace Doczy.Business.DTOs.Experiance
{
    public record CreateExperianceDto
    (
        string Title,       
        Guid HospitalId,
        string Location,
        bool currentlyWorking,
        DateTime StartDate,
        DateTime EndDate
        );
}
