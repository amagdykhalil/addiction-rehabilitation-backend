using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Commands.RemoveRoles
{
    public class RemoveRolesCommandValidator : AbstractValidator<RemoveRolesCommand>
    {
        public RemoveRolesCommandValidator(IStringLocalizer<RemoveRolesCommandValidator> localizer)
        {

            RuleFor(x => x.RoleIds)
                .NotNull().WithMessage(localizer[LocalizationKeys.Validation.Required])
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required]);

            RuleForEach(x => x.RoleIds)
                .SetValidator(new IdValidator<RemoveRolesCommand>(localizer));
        }
    }
}