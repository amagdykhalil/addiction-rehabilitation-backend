using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(
            IStringLocalizer<LoginCommandValidator> localizer,
            IIdentityService identityService)
        {
            RuleFor(l => l.Email)
                .SetValidator(new CustomEmailValidator<LoginCommand>(localizer, true));

            RuleFor(l => l.Password)
                .NotEmpty()
                .WithMessage(localizer[LocalizationKeys.Validation.Required])
                .MaximumLength(50)
                .WithMessage(localizer[LocalizationKeys.Validation.PasswordTooLong]);
        }
    }
}
