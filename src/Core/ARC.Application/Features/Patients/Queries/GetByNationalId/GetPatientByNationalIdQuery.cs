using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetByNationalId
{
    public record GetPatientByNationalIdQuery(string NationalId) : IQuery<PatientDetailsDto>;
} 