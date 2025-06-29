namespace ARC.Application.Features.Patients.Queries.GetByPassport
{
    public class GetPatientByPassportQueryValidator : AbstractValidator<GetPatientByPassportQuery>
    {
        public GetPatientByPassportQueryValidator(IStringLocalizer<GetPatientByPassportQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[A-Z0-9]{6,9}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
}