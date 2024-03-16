using Doczy.Business.DTOs.AwardDtos;
using Doczy.Business.DTOs.EducationDtos;
using Doczy.Business.DTOs.Experiance;

namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetDoctorResumeDto
    {
        public ICollection<GetExperianceDto>? Experiances { get; set; }
        public ICollection<EducationDto>? Educations { get; set; }
        public ICollection<GetAwardDto>? Awards { get; set; }
    }
}
