using ARC.Application.Common.Validator;
using FluentValidation;

namespace ARC.Application.Features.Auth.Commands.ChangeEmail
{
    public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
    {
        public ChangeEmailCommandValidator(IStringLocalizer<ChangeEmailCommand> localizer)
        {
            RuleFor(x => x.NewEmail)
                .SetValidator(new CustomEmailValidator<ChangeEmailCommand>(localizer, true));
        }
    }
} 