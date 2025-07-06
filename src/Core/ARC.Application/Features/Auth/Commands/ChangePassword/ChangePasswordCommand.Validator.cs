

using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator(
            IStringLocalizer<ChangePasswordCommandValidator> localizer,
            IIdentityService identityService)
        {
            RuleFor(x => x.UserId)
                .SetValidator(new IdValidator<ChangePasswordCommand>(localizer));

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required]);

            RuleFor(x => x.NewPassword)
                .SetAsyncValidator(new PasswordValidator<ChangePasswordCommand>(identityService, localizer));
        }
    }
}
