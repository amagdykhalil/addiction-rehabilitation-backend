using ARC.Application.Abstractions.Repositories;
using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetByPassport
{
    public class GetPatientByPassportQueryHandler : IQueryHandler<GetPatientByPassportQuery, PatientDetailsDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<GetPatientByPassportQueryHandler> _localizer;

        public GetPatientByPassportQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<GetPatientByPassportQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<PatientDetailsDto>> Handle(GetPatientByPassportQuery query, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByPassportNumberAsync(query.PassportNumber);

            if (patient == null)
            {
                return Result.Error(_localizer[LocalizationKeys.Patient.NotFoundByPassport, query.PassportNumber]);
            }

            return Result.Success(patient.ToDto());
        }
    }
}