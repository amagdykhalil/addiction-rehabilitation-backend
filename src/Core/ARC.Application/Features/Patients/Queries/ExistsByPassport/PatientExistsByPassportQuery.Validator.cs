namespace ARC.Application.Features.Patients.Queries.ExistsByPassport
{
    public class PatientExistsByPassportQueryValidator : AbstractValidator<PatientExistsByPassportQuery>
    {
        public PatientExistsByPassportQueryValidator(IStringLocalizer<PatientExistsByPassportQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[A-Z0-9]{6,9}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
}