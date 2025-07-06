

namespace ARC.Application.Features.Patients.Commands.Delete
{
    public record DeletePatientCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
} 