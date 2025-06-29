using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Commands.Delete
{
    public class DeletePatientCommandHandler : ICommandHandler<DeletePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<DeletePatientCommandHandler> _logger;
        private readonly IStringLocalizer<DeletePatientCommandHandler> _localizer;

        public DeletePatientCommandHandler(IPatientRepository patientRepository, ILogger<DeletePatientCommandHandler> logger, IStringLocalizer<DeletePatientCommandHandler> localizer)
        {
            _patientRepository = patientRepository;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
        {
            await _patientRepository.DeleteAsync(command.Id);

            _logger.LogInformation("Deleted patient with ID: {PatientId}", (command.Id));
            return Result.Success(true);
        }
    }
}