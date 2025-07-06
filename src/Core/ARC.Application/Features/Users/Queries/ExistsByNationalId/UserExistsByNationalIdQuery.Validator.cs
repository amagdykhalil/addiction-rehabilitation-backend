namespace ARC.Application.Features.Users.Queries.ExistsByNationalId
{
    public class UserExistsByNationalIdQueryValidator : AbstractValidator<UserExistsByNationalIdQuery>
    {
        public UserExistsByNationalIdQueryValidator(IStringLocalizer<UserExistsByNationalIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.NationalIdNumber)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[0-9]{14}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
}