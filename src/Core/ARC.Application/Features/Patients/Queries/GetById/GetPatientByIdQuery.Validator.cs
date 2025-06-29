using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Patients.Queries.GetById
{
    public class GetPatientByIdQueryValidator : AbstractValidator<GetPatientByIdQuery>
    {
        public GetPatientByIdQueryValidator(IStringLocalizer<GetPatientByIdQueryValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<GetPatientByIdQuery>(validationLocalizer));
        }
    }
}