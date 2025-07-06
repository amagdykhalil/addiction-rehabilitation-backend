using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Queries.GetAllRoles
{
    public class GetAllUserRolesQueryValidator : AbstractValidator<GetAllUserRolesQuery>
    {
        public GetAllUserRolesQueryValidator(IStringLocalizer<GetAllUserRolesQueryValidator> localizer)
        {
            RuleFor(u => u.UserId)
            .SetValidator(new IdValidator<GetAllUserRolesQuery>(localizer));
        }
    }
} 