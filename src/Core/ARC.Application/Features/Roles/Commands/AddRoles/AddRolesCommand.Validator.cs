using FluentValidation;
using Microsoft.Extensions.Localization;
using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Roles.Commands.AddRoles
{
    public class AddUserRolesCommandValidator : AbstractValidator<AddRolesCommand>
    {
        public AddUserRolesCommandValidator(IStringLocalizer<AddUserRolesCommandValidator> localizer)
        {
            RuleFor(x => x.Roles)
                .NotNull().WithMessage(localizer[LocalizationKeys.Validation.Required])
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required]);

            RuleForEach(x => x.Roles)
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required])
                .ChildRules(role =>
                {
                    role.RuleFor(r => r.Name_en)
                        .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required])
                        .Length(1, 256).WithMessage(localizer[LocalizationKeys.Validation.BetweenLength, 1, 256]);
                    role.RuleFor(r => r.Name_ar)
                        .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required])
                        .Length(1, 256).WithMessage(localizer[LocalizationKeys.Validation.BetweenLength, 1, 256]);
                });
        }
    }
} 