using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Queries.ExistsByPassport
{
    public class PatientExistsByPassportQueryHandler : IQueryHandler<PatientExistsByPassportQuery, int?>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<PatientExistsByPassportQueryHandler> _localizer;

        public PatientExistsByPassportQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<PatientExistsByPassportQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<int?>> Handle(PatientExistsByPassportQuery request, CancellationToken cancellationToken)
        {
            var id = await _patientRepository.isExistsByPasswordAsync(request.PassportNumber,cancellationToken);

            if (id == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.Patient.NotFoundByPassport, request.PassportNumber]);
            }

            return Result.Success(id);
        }
    }
}