using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(
            IStringLocalizer<CreateUserCommandValidator> validationLocalizer,
            IIdentityService identityService)
        {
            // Person data validation
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

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage(validationLocalizer[LocalizationKeys.Validation.IsInEnum]);

            RuleFor(x => x.CallPhoneNumber)
                .SetValidator(new PhoneNumberValidator<CreateUserCommand>(validationLocalizer));

            RuleFor(x => x.NationalityId)
                .SetValidator(new IdValidator<CreateUserCommand>(validationLocalizer));

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

            // User data validation
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .SetValidator(new CustomEmailValidator<CreateUserCommand>(validationLocalizer, true));

            RuleForEach(x => x.Roles)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .When(x => x.Roles != null);
        }
    }
}