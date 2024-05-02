namespace Doczy.Business.DTOs.EducationDtos
{
	public record UpdateEducationDto(
        Guid UnivercityId ,
        Guid UnivercityDegreeId ,
        Guid FieldOfStudyId ,
        DateTime StartDate ,
        DateTime EndDate 
	);
}

