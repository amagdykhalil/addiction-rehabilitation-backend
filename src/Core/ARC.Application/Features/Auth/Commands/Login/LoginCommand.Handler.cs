using ARC.Application.Features.Auth.Models;

namespace ARC.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler(
        IIdentityService identityService,
        ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork,
        ILogger<LoginCommandHandler> logger,
        IStringLocalizer<LoginCommandHandler> localizer)
        : ICommandHandler<LoginCommand, AuthDTO>
    {
        public async Task<Result<AuthDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserAsync(request.Email, request.Password, cancellationToken);

            if (user == null)
            {
                return Result<AuthDTO>.Error(localizer[LocalizationKeys.Auth.InvalidCredentials]);
            }

            if (!await identityService.IsEmailConfirmedAsync(user, cancellationToken))
            {
                return Result<AuthDTO>.Unauthorized(localizer[LocalizationKeys.Auth.ShouldConfirmEmail]);
            }

            if (user.DeletedAt != null)
            {
                return Result<AuthDTO>.Unauthorized(localizer[LocalizationKeys.Auth.InactiveUser]);
            }

            var accessToken = await tokenProvider.Create(user);
            AuthDTO AuthInfo = new AuthDTO
            {
                AccessToken = accessToken,
                UserId = user.Id,
                ExpiresOn = tokenProvider.GetAccessTokenExpiration(),
            };

            var ActiveRefreshToken = await refreshTokenRepository.GetActiveRefreshTokenAsync(user.Id, cancellationToken);

            if (ActiveRefreshToken != null)
            {
                AuthInfo.RefreshToken = ActiveRefreshToken.Token;
                AuthInfo.RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = refreshTokenRepository.GenerateRefreshToken(user.Id);

                await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

                AuthInfo.RefreshToken = refreshToken.Token;
                AuthInfo.RefreshTokenExpiration = refreshToken.ExpiresOn;

                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Result<AuthDTO>.Success(AuthInfo);
        }
    }
}

