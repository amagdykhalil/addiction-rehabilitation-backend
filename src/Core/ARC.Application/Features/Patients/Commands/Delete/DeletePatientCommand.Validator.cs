using ARC.Application.Common.Validator;

namespace ARC.Application.Features.Patients.Commands.Delete
{
    public class DeletePatientCommandValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator(IStringLocalizer<DeletePatientCommandValidator> validationLocalizer)
        {
            RuleFor(x => x.Id)
                .SetValidator(new IdValidator<DeletePatientCommand>(validationLocalizer));
        }
    }
}