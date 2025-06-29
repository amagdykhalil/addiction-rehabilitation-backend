using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Patients.Queries.ExistsByNationalId
{
    public class ExistsByNationalIdQueryValidator : AbstractValidator<ExistsByNationalIdQuery>
    {
        public ExistsByNationalIdQueryValidator(IStringLocalizer<ExistsByNationalIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[0-9]{14}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
} 