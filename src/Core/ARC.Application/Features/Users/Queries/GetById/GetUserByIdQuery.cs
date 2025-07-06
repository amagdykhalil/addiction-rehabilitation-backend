namespace ARC.Application.Features.Users.Queries.GetById
{
    public record GetUserByIdQuery : IQuery<UserDetailsDto>
    {
        public int Id { get; set; }
    }
}