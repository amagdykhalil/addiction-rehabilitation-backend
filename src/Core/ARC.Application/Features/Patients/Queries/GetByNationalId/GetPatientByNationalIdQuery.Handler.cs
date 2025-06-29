using ARC.Application.Abstractions.Repositories;
using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetByNationalId
{
    public class GetPatientByNationalIdQueryHandler : IQueryHandler<GetPatientByNationalIdQuery, PatientDetailsDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<GetPatientByNationalIdQueryHandler> _localizer;

        public GetPatientByNationalIdQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<GetPatientByNationalIdQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<PatientDetailsDto>> Handle(GetPatientByNationalIdQuery query, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByNationalIdAsync(query.NationalId);

            if (patient == null)
            {
                return Result.Error(_localizer[LocalizationKeys.Patient.NotFoundByNationalId, query.NationalId]);
            }

            return Result.Success(patient.ToDto());
        }
    }
}