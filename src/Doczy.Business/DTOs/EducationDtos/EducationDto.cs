using System;
namespace Doczy.Business.DTOs.EducationDtos
{
	public record EducationDto
	(
        string Doctor,
        string Univercity,
        string UnivercityDegree,
        string FieldOfStudy,
        DateTime StartDate,
        DateTime EndDate
    );
}

