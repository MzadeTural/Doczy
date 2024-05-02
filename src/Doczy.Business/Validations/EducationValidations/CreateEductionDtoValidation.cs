using System;
using Doczy.Business.DTOs.EducationDtos;
using FluentValidation;

namespace Doczy.Business.Validations.EducationValidations
{
	public class UpdateEductionDtoValidation:AbstractValidator<UpdateEducationDto>
	{
		public UpdateEductionDtoValidation()
		{
			RuleFor(x => x.UnivercityId).NotEmpty().NotNull();
			RuleFor(x => x.UnivercityDegreeId).NotEmpty().NotNull();
			RuleFor(x => x.FieldOfStudyId).NotEmpty().NotNull();
			RuleFor(x => x.StartDate).NotEmpty().NotNull();
			RuleFor(x => x.EndDate).NotEmpty().NotNull();
        }
	}
}

