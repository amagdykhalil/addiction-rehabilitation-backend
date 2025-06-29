using FluentValidation;
using Microsoft.Extensions.Localization;
using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Patients.Queries.ExistsById
{
    public class ExistsByIdQueryValidator : AbstractValidator<ExistsByIdQuery>
    {
        public ExistsByIdQueryValidator(IStringLocalizer<ExistsByIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<ExistsByIdQuery>(validationLocalizer));
        }
    }
} 