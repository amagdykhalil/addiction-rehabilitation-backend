using ARC.Shared.Keys;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(IStringLocalizer<LoginCommandValidator> localizer)
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .WithMessage(localizer[LocalizationKeys.EmailRequired])
                .EmailAddress()
                .WithMessage(localizer[LocalizationKeys.InvalidEmail]);

            RuleFor(l => l.PasswordHash)
                .NotEmpty()
                .WithMessage(localizer[LocalizationKeys.PasswordRequired]);
        }
    }
}

