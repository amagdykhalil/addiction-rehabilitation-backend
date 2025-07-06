namespace ARC.Application.Features.Auth.Commands.ForgotPassword
{
    public record ForgotPasswordCommand(string Email) : ICommand;
}