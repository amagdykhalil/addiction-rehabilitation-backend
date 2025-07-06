namespace ARC.Application.Features.Users.Queries.GetByEmail
{
    public record UserExitsByEmailQuery : IQuery<int?>
    {
        public string Email { get; set; }
    }
}