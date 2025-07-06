namespace ARC.Application.Features.Users.Commands.Delete
{
    public record DeleteUserCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
} 