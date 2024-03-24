using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.SpecialityDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetAboutDoctorDto
    {
        public string? AboutDoctor { get; set; }
        public IList<GetLanguageDto>? Languages { get; set; }
        public IList<GetSpecialityDto>? Specialities { get; set; }
    }
}
