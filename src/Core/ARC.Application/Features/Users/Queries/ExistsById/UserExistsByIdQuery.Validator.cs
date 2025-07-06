using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Queries.ExistsById
{
    public class UserExistsByIdQueryValidator : AbstractValidator<UserExistsByIdQuery>
    {
        public UserExistsByIdQueryValidator(IStringLocalizer<UserExistsByIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<UserExistsByIdQuery>(validationLocalizer));
        }
    }
}