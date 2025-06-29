using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Auth.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandValidator : AbstractValidator<ResendConfirmationEmailCommand>
    {
        public ResendConfirmationEmailCommandValidator(
            IStringLocalizer<ResendConfirmationEmailCommandValidator> localizer)
        {
            RuleFor(r => r.Email)
                .SetValidator(new CustomEmailValidator<ResendConfirmationEmailCommand>(localizer));
        }
    }
}