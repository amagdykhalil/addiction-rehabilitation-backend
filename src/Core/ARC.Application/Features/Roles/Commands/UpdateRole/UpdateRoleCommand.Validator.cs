using FluentValidation;
using Microsoft.Extensions.Localization;
using ARC.Shared.Keys;
using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator(IStringLocalizer<UpdateRoleCommandValidator> localizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<UpdateRoleCommand>(localizer));
                
            RuleFor(x => x.Name_en)
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required])
                .Length(1, 256).WithMessage(localizer[LocalizationKeys.Validation.BetweenLength, 1, 256]);
            RuleFor(x => x.Name_ar)
                .NotEmpty().WithMessage(localizer[LocalizationKeys.Validation.Required])
                .Length(1, 256).WithMessage(localizer[LocalizationKeys.Validation.BetweenLength, 1, 256]);
        }
    }
}
