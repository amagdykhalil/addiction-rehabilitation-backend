namespace ARC.Application.Features.Users.Commands.Reactivate
{
    public record ReactivateUserCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
} 