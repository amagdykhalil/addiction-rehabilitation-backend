using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetByPassport
{
    public record GetPatientByPassportQuery(string PassportNumber) : IQuery<PatientDetailsDto>;
}