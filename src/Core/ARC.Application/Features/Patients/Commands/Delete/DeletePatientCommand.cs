

namespace ARC.Application.Features.Patients.Commands.Delete
{
    public record DeletePatientCommand(int Id) : ICommand<bool>;
} 