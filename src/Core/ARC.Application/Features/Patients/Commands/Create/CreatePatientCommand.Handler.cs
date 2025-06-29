using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Commands.Create
{
    public class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, int>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePatientCommandHandler> _logger;
        private readonly IStringLocalizer<CreatePatientCommand> stringLocalizer;

        public CreatePatientCommandHandler(
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreatePatientCommandHandler> logger,
            IStringLocalizer<CreatePatientCommand> stringLocalizer)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            this.stringLocalizer = stringLocalizer;
        }

        public async Task<Result<int>> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = command.MapToPatient();
            await _patientRepository.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (patient.Id == 0)
            {
                return Result.Error(stringLocalizer[LocalizationKeys.Patient.CreationFail]);
            }

            _logger.LogInformation("Created new patient with ID: {PatientId}", patient.Id);
            return Result.Success(patient.Id);
        }
    }
}