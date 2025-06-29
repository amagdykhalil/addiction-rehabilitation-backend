using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Patients.Queries.ExistsByPassport
{
    public class ExistsByPassportQueryValidator : AbstractValidator<ExistsByPassportQuery>
    {
        public ExistsByPassportQueryValidator(IStringLocalizer<ExistsByPassportQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[A-Z0-9]{6,9}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
} 