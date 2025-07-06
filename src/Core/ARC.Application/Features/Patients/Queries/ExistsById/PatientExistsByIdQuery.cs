namespace ARC.Application.Features.Patients.Queries.ExistsById
{
    public record PatientExistsByIdQuery : IQuery<bool>
    {
        public int Id { get; init; }

    }
}