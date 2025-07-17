using ARC.Application.Abstractions.Persistence;
using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Commands.Update
{
    public class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdatePatientCommandHandler> _logger;
        private readonly IStringLocalizer<UpdatePatientCommandHandler> _localizer;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork, ILogger<UpdatePatientCommandHandler> logger, IStringLocalizer<UpdatePatientCommandHandler> localizer)
        {
            _patientRepository = patientRepository;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdWithPersonAsync(command.Id, cancellationToken);

            if (patient == null)
                return Result.Error(_localizer[LocalizationKeys.Patient.NotFound]);

            command.MapToEntities(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated patient with ID: {PatientId}", patient.Id);

            return Result.Success(true);
        }
    }
}