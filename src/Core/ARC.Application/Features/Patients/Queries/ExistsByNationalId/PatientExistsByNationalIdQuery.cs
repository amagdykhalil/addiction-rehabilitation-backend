namespace ARC.Application.Features.Patients.Queries.ExistsByNationalId
{
    public record PatientExistsByNationalIdQuery : IQuery<int?>
    {
        public string NationalId { get; init; }


    }
}