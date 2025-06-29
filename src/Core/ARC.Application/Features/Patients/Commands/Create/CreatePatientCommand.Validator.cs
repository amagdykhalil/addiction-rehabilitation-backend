using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Patients.Commands.Create
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator(IStringLocalizer<CreatePatientCommandValidator> validationLocalizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Length(2, 50).WithMessage(validationLocalizer[LocalizationKeys.Validation.BetweenLength, 2, 50]);

            RuleFor(x => x.SecondName)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Length(2, 50).WithMessage(validationLocalizer[LocalizationKeys.Validation.BetweenLength, 2, 50]);

            RuleFor(x => x.ThirdName)
                .Length(2, 50).When(x => !string.IsNullOrEmpty(x.ThirdName))
                .WithMessage(validationLocalizer[LocalizationKeys.Validation.BetweenLength, 2, 50]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Length(2, 50).WithMessage(validationLocalizer[LocalizationKeys.Validation.BetweenLength, 2, 50]);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Must(date => date < DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage(validationLocalizer[LocalizationKeys.Validation.MustBeInPast]);

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage(validationLocalizer[LocalizationKeys.Validation.IsInEnum]);

            RuleFor(x => x.CallPhoneNumber)
                .SetValidator(new PhoneNumberValidator<CreatePatientCommand>(validationLocalizer));

            RuleFor(x => x.NationalityId)
                .SetValidator(new IdValidator<CreatePatientCommand>(validationLocalizer));

            When(x => !string.IsNullOrEmpty(x.NationalIdNumber), () =>
            {
                RuleFor(x => x.NationalIdNumber)
                    .Matches(@"^[0-9]{14}$")
                    .WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
            });

            When(x => !string.IsNullOrEmpty(x.PassportNumber), () =>
            {
                RuleFor(x => x.PassportNumber)
                    .Matches(@"^[A-Z0-9]{6,9}$")
                    .WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
            });

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.NationalIdNumber) || !string.IsNullOrWhiteSpace(x.PassportNumber))
                .WithMessage(validationLocalizer[LocalizationKeys.Validation.IdentificationRequired]);
        }
    }
}