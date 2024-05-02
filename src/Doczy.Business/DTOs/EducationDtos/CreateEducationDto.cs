namespace Doczy.Business.DTOs.EducationDtos
{
	public record CreateEducationDto(
        Guid UnivercityId ,
        Guid UnivercityDegreeId ,
        Guid FieldOfStudyId ,
        DateTime StartDate ,
        DateTime EndDate 
	);
}

