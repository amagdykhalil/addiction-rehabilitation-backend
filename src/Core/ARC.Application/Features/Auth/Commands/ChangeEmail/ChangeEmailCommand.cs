namespace ARC.Application.Features.Auth.Commands.ChangeEmail
{
    public record ChangeEmailCommand(int UserId, string NewEmail) : ICommand;
} 