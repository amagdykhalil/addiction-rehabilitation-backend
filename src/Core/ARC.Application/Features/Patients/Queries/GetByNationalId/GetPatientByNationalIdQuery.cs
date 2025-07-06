using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetByNationalId
{
    public record GetPatientByNationalIdQuery : IQuery<PatientDetailsDto>
    {
        public string NationalId { get; set; }
    }
} 