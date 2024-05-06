using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.EducationDtos;
using FluentValidation;

namespace Doczy.Business.Validations.DoctorValidations
{
    public class UpdateAboutDoctorValidation: AbstractValidator<UpdateAboutDoctorDto>
    {
        public UpdateAboutDoctorValidation()
        {
            RuleFor(x => x.AboutDoctor).MaximumLength(500);
        }
    }
}
