namespace ARC.Application.Features.Auth.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(int UserId, string Code, string? ChangedEmail = null) : ICommand;
}