using ARC.Application.Abstractions.Repositories;
using Application.Abstractions.Messaging;
using Microsoft.Extensions.Localization;

namespace ARC.Application.Features.Patients.Queries.ExistsByPassport
{
    public class ExistsByPassportQueryHandler : IQueryHandler<ExistsByPassportQuery, int?>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IStringLocalizer<ExistsByPassportQueryHandler> _localizer;

        public ExistsByPassportQueryHandler(
            IPatientRepository patientRepository,
            IStringLocalizer<ExistsByPassportQueryHandler> localizer)
        {
            _patientRepository = patientRepository;
            _localizer = localizer;
        }

        public async Task<Result<int?>> Handle(ExistsByPassportQuery request, CancellationToken cancellationToken)
        {
            var id = await _patientRepository.isExistsByPassword(request.PassportNumber);
            
            if (id == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.Patient.NotFoundByPassport, request.PassportNumber]);
            }
            
            return Result.Success(id);
        }
    }
} 