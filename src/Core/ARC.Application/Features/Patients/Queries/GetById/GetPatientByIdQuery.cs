
using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetById
{
    public record GetPatientByIdQuery : IQuery<PatientDetailsDto>
    {
        public int Id { get; set; }
    }
} 