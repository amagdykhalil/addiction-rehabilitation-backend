using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Queries.ExistsByNationalId
{
    public class PatientExistsByNationalIdQueryHandler : IQueryHandler<PatientExistsByNationalIdQuery, int?>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<PatientExistsByNationalIdQueryHandler> _localizer;

        public PatientExistsByNationalIdQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<PatientExistsByNationalIdQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<int?>> Handle(PatientExistsByNationalIdQuery request, CancellationToken cancellationToken)
        {
            var id = await _patientRepository.isExistsByNationalIdNumber(request.NationalId,cancellationToken);

            if (id == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.Patient.NotFoundByNationalId, request.NationalId]);
            }

            return Result.Success(id);
        }
    }
}