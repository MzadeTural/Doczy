namespace Doczy.Business.DTOs.Experiance
{
    public record CreateExperianceDto
    (
        string Title,       
        Guid WorkPlaceId,
        string Location,
        bool currentlyWorking,
        DateTime StartDate,
        DateTime EndDate
        );
}
