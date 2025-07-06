namespace ARC.Application.Features.Patients.Queries.ExistsByPassport
{
    public record PatientExistsByPassportQuery : IQuery<int?>
    {
        public string PassportNumber { get; init; }

    }
}