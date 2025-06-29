using ARC.Application.Features.Auth.Models;

namespace ARC.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string Token) : ICommand<AuthDTO>;
}

