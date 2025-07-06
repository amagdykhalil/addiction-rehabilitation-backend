using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Patients.Queries.ExistsById
{
    public class PatientExistsByIdQueryValidator : AbstractValidator<PatientExistsByIdQuery>
    {
        public PatientExistsByIdQueryValidator(IStringLocalizer<PatientExistsByIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<PatientExistsByIdQuery>(validationLocalizer));
        }
    }
}