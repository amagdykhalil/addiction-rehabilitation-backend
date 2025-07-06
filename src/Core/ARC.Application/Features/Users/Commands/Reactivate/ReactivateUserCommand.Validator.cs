using FluentValidation;
using Microsoft.Extensions.Localization;
using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Commands.Reactivate
{
    public class ReactivateUserCommandValidator : AbstractValidator<ReactivateUserCommand>
    {
        public ReactivateUserCommandValidator(IStringLocalizer<ReactivateUserCommandValidator> localizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<ReactivateUserCommand>(localizer));
        }
    }
} 