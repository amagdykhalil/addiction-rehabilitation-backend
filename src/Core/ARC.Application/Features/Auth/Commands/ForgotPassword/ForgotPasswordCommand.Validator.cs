using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator(
            IStringLocalizer<ForgotPasswordCommandValidator> localizer)
        {
            RuleFor(f => f.Email)
                .SetValidator(new CustomEmailValidator<ForgotPasswordCommand>(localizer, true));
        }
    }
}