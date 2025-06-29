namespace ARC.Application.Features.Patients.Queries.ExistsByNationalId
{
    public record ExistsByNationalIdQuery : IQuery<int?>
    {
        public string NationalId { get; init; }


    }
}