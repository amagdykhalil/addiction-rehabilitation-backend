using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Users.Queries.GetByEmail
{
    public class UserExitsByEmailQueryValidator : AbstractValidator<UserExitsByEmailQuery>
    {
        public UserExitsByEmailQueryValidator(IStringLocalizer<UserExitsByEmailQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.Email)
                .SetValidator(new CustomEmailValidator<UserExitsByEmailQuery>(validationLocalizer, true));
        }
    }
}