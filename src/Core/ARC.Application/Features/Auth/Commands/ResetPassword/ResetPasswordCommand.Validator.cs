using ARC.Application.Common.Validator;
using ARC.Application.Features.Auth.Common;

namespace ARC.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator(
            IStringLocalizer<ResetPasswordCommandValidator> localizer,
            IIdentityService identityService)
        {
            RuleFor(r => r.Email)
                .SetValidator(new CustomEmailValidator<ResetPasswordCommand>(localizer));

            RuleFor(r => r.ResetCode)
                .SetValidator(new CodeValidator<ResetPasswordCommand>(localizer));

            RuleFor(r => r.NewPassword)
                .SetAsyncValidator(new PasswordValidator<ResetPasswordCommand>(identityService, localizer));
        }
    }
}