using ARC.Application.Features.Patients.Queries.Models;

namespace ARC.Application.Features.Patients.Queries.GetByPassport
{
    public record GetPatientByPassportQuery: IQuery<PatientDetailsDto>
    {
        public string PassportNumber { get; set; }
    }
}