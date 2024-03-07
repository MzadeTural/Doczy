using System;
using Doczy.Business.DTOs.UnivercityDtos;
using FluentValidation;

namespace Doczy.Business.Validations.UnivercityValidation
{
	public class CreateUnivercityDtoValidation : AbstractValidator<CreateUnivercityDto>
	{
		public CreateUnivercityDtoValidation()
		{
            RuleFor(x => x.Name).MaximumLength(50);
			RuleFor(x => x.IconUrl).MaximumLength(150);
		}
	}
}

