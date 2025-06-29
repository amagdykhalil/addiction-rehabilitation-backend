using ARC.Application.Common.Validator;
using ARC.Application.Features.Auth.Common;

namespace ARC.Application.Features.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator(IStringLocalizer<ConfirmEmailCommandValidator> localizer)
        {
            RuleFor(x => x.UserId)
                .SetValidator(new IdValidator<ConfirmEmailCommand>(localizer));

            RuleFor(x => x.Code)
                .SetValidator(new CodeValidator<ConfirmEmailCommand>(localizer));

            RuleFor(x => x.ChangedEmail)
                .SetValidator(new CustomEmailValidator<ConfirmEmailCommand>(localizer));
        }
    }
}