using ARC.Application.Abstractions.Repositories;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Patients.Queries.ExistsById
{
    public class ExistsByIdQueryHandler : IQueryHandler<ExistsByIdQuery, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<ExistsByIdQueryHandler> _localizer;

        public ExistsByIdQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<ExistsByIdQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(ExistsByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);
            
            if (patient == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.Patient.NotFoundById, request.Id]);
            }
            
            return Result.Success(true);
        }
    }
} 