using ARC.Application.Features.Auth.Models;

namespace ARC.Application.Features.Auth.Commands.Login
{
    public record LoginCommand(string Email, string Password) : ICommand<AuthDTO>;
}

