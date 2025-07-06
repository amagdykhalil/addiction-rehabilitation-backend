using ARC.Application.Common.Validator;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Users.Queries.GetById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator(IStringLocalizer<GetUserByIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<GetUserByIdQuery>(validationLocalizer));
        }
    }
} 