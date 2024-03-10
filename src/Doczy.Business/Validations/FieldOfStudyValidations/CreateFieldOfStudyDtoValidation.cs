using Doczy.Business.DTOs.FieldOfStudyDtos;
using FluentValidation;

namespace Doczy.Business.Validations.FieldOfStudyValidations
{
    public class CreateFieldOfStudyDtoValidation : AbstractValidator<CreateFieldOfStudyDto>
	{
		public CreateFieldOfStudyDtoValidation()
		{
			RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
		}
	}
}