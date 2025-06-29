using ARC.Application.Abstractions.Repositories;
using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetById
{
    public class GetPatientByIdQueryHandler : IQueryHandler<GetPatientByIdQuery, PatientDetailsDto>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<GetPatientByIdQueryHandler> _localizer;

        public GetPatientByIdQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<GetPatientByIdQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<PatientDetailsDto>> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdWithPersonAsync(query.Id);

            if (patient == null)
            {
                return Result.Error(_localizer[LocalizationKeys.Patient.NotFoundById, query.Id]);
            }

            return Result.Success(patient.ToDto());
        }
    }
}