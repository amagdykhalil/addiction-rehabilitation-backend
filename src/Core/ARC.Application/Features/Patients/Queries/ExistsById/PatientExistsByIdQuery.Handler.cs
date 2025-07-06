using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Queries.ExistsById
{
    public class PatientExistsByIdQueryHandler : IQueryHandler<PatientExistsByIdQuery, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<PatientExistsByIdQueryHandler> _localizer;

        public PatientExistsByIdQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<PatientExistsByIdQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(PatientExistsByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

            if (patient == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.Patient.NotFoundById, request.Id]);
            }

            return Result.Success(true);
        }
    }
}