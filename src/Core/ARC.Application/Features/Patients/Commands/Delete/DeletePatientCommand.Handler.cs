using ARC.Application.Abstractions.Repositories;

namespace ARC.Application.Features.Patients.Commands.Delete
{
    public class DeletePatientCommandHandler : ICommandHandler<DeletePatientCommand, bool>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<DeletePatientCommandHandler> _logger;

        public DeletePatientCommandHandler(IPatientRepository patientRepository, ILogger<DeletePatientCommandHandler> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }

        public async Task<Result<bool>> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
        {
            await _patientRepository.DeleteAsync(command.Id, cancellationToken);

            _logger.LogInformation("Deleted patient with ID: {PatientId}", (command.Id));
            return Result.Success(true);
        }
    }
}