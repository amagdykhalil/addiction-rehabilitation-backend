namespace ARC.Application.Features.Patients.Queries.ExistsByNationalId
{
    public class PatientExistsByNationalIdQueryValidator : AbstractValidator<PatientExistsByNationalIdQuery>
    {
        public PatientExistsByNationalIdQueryValidator(IStringLocalizer<PatientExistsByNationalIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[0-9]{14}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
}