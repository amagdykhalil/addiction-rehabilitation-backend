namespace ARC.Application.Features.Users.Queries.ExistsById
{
    public record UserExistsByIdQuery : IQuery<bool>
    {
        public int Id { get; set; }
    }
}