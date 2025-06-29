using ARC.Application.Abstractions.Repositories;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Patients.Queries.ExistsByNationalId
{
    public class ExistsByNationalIdQueryHandler : IQueryHandler<ExistsByNationalIdQuery, int?>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<ExistsByNationalIdQueryHandler> _localizer;

        public ExistsByNationalIdQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<ExistsByNationalIdQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<int?>> Handle(ExistsByNationalIdQuery request, CancellationToken cancellationToken)
        {
            var id = await _patientRepository.isExistsByNationalIdNumber(request.NationalId);
            
            if (id == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.Patient.NotFoundByNationalId, request.NationalId]);
            }
            
            return Result.Success(id);
        }
    }
} 