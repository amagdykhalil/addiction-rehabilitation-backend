using FluentValidation;
using Microsoft.Extensions.Localization;
using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Commands.UpdateUserRoles
{
    public class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
    {
        public UpdateUserRolesCommandValidator(IStringLocalizer<UpdateUserRolesCommandValidator> localizer)
        {
            RuleFor(x => x.UserId)
                .SetValidator(new IdValidator<UpdateUserRolesCommand>(localizer));
            RuleFor(x => x.RoleIds)
                .NotNull().WithMessage(localizer[LocalizationKeys.Validation.Required])
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required]);
            RuleForEach(x => x.RoleIds)
                .SetValidator(new IdValidator<UpdateUserRolesCommand>(localizer));
        }
    }
} 