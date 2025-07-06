using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidator(IStringLocalizer<GetRoleByIdQueryValidator> localizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<GetRoleByIdQuery>(localizer));
        }
    }
}
