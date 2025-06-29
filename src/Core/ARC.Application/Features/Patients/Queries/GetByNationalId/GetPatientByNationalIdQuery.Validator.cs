namespace ARC.Application.Features.Patients.Queries.GetByNationalId
{
    public class GetPatientByNationalIdQueryValidator : AbstractValidator<GetPatientByNationalIdQuery>
    {
        public GetPatientByNationalIdQueryValidator(IStringLocalizer<GetPatientByNationalIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage(validationLocalizer[LocalizationKeys.Validation.Required])
                .Matches(@"^[0-9]{14}$").WithMessage(validationLocalizer[LocalizationKeys.Validation.InvalidFormat]);
        }
    }
}