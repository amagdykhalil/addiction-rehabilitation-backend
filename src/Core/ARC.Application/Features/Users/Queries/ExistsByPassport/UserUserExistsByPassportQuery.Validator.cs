namespace ARC.Application.Features.Users.Queries.ExistsByPassport
{
    public class UserExistsByPassportQueryValidator : AbstractValidator<UserExistsByPassportQuery>
    {
        public UserExistsByPassportQueryValidator(IStringLocalizer<UserExistsByPassportQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[A-Z0-9]{6,9}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
}