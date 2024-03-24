using System;
namespace Doczy.Business.DTOs.EducationDtos
{
	//public record EducationDto
	//(
 //       string Doctor,
 //       string Univercity,
 //       string UnivercityDegree,
 //       string FieldOfStudy,
 //       DateTime StartDate,
 //       DateTime EndDate
 //   );
    public class EducationDto
    {
        public Guid Id { get; set; }
        public string Univercity { get; set; }
        public string UnivercityDegree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

