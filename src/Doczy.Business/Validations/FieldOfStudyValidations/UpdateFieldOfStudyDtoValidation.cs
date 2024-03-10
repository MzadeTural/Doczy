using Doczy.Business.DTOs.FieldOfStudyDtos;
using FluentValidation;

namespace Doczy.Business.Validations.FieldOfStudyValidations
{
    public class UpdateFieldOfStudyDtoValidation : AbstractValidator<UpdateFieldOfStudyDto>
	{
		public UpdateFieldOfStudyDtoValidation()
		{
			RuleFor(x => x.Name).MaximumLength(50);
		}
	}
}