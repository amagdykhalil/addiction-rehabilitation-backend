using ARC.Application.Features.Auth.Commands.Login;

namespace ARC.Application.Tests.Features.Auth.Commands
{
    public class LoginCommandHandlerTests : IClassFixture<LocalizationKeyFixture>
    {

        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<ITokenProvider> _tokenProviderMock;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<LoginCommandHandler>> _loggerMock;
        private readonly Mock<IStringLocalizer<LoginCommandHandler>> _localizerMock;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _tokenProviderMock = new Mock<ITokenProvider>();
            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<LoginCommandHandler>>();
            _localizerMock = new Mock<IStringLocalizer<LoginCommandHandler>>();

            // Setup default localization
            var localizedString = new LocalizedString(LocalizationKeys.Auth.InvalidCredentials, "Email or password is incorrect!");
            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidCredentials])
                .Returns(localizedString);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.ShouldConfirmEmail])
                .Returns(new LocalizedString(LocalizationKeys.Auth.ShouldConfirmEmail, "Please confirm your email first!"));

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InactiveUser])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InactiveUser, "User account is inactive!"));

            _handler = new LoginCommandHandler(
                _identityServiceMock.Object,
                _tokenProviderMock.Object,
                _refreshTokenRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object,
                _localizerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidCredentialsAndNoRefreshToken_ReturnsNewAccessAndRefreshToken()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "hashedPassword");
            var user = new User { Id = 1, Email = command.Email, DeletedAt = null };
            var accessToken = "access-token";
            var tokenExpiration = DateTime.UtcNow.AddHours(1);

            _identityServiceMock.Setup(x => x.GetUserAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _tokenProviderMock.Setup(x => x.Create(user))
                .ReturnsAsync(accessToken);

            _tokenProviderMock.Setup(x => x.GetAccessTokenExpiration())
                .Returns(tokenExpiration);

            _refreshTokenRepositoryMock.Setup(x => x.GetActiveRefreshTokenAsync(user.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((RefreshToken?)null);

            var newRefreshToken = new RefreshToken
            {
                Token = "new-refresh-token",
                ExpiresOn = DateTime.UtcNow.AddDays(7)
            };

            _refreshTokenRepositoryMock.Setup(x => x.GenerateRefreshToken(user.Id))
                .Returns(newRefreshToken);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(accessToken, result.Value.AccessToken);
            Assert.Equal(user.Id, result.Value.UserId);
            Assert.Equal(tokenExpiration, result.Value.ExpiresOn);
            Assert.Equal(newRefreshToken.Token, result.Value.RefreshToken);
            Assert.Equal(newRefreshToken.ExpiresOn, result.Value.RefreshTokenExpiration);

            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ValidCredentialsAndExistingRefreshToken_ReturnsAccessAndExistingRefreshToken()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "hashedPassword");
            var user = new User { Id = 1, Email = command.Email, DeletedAt = null };
            var accessToken = "access-token";
            var tokenExpiration = DateTime.UtcNow.AddHours(1);
            var existingRefreshToken = new RefreshToken
            {
                Token = "existing-refresh-token",
                ExpiresOn = DateTime.UtcNow.AddDays(7)
            };

            _identityServiceMock.Setup(x => x.GetUserAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _tokenProviderMock.Setup(x => x.Create(user))
                .ReturnsAsync(accessToken);

            _tokenProviderMock.Setup(x => x.GetAccessTokenExpiration())
                .Returns(tokenExpiration);

            _refreshTokenRepositoryMock.Setup(x => x.GetActiveRefreshTokenAsync(user.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingRefreshToken);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(accessToken, result.Value.AccessToken);
            Assert.Equal(user.Id, result.Value.UserId);
            Assert.Equal(tokenExpiration, result.Value.ExpiresOn);
            Assert.Equal(existingRefreshToken.Token, result.Value.RefreshToken);
            Assert.Equal(existingRefreshToken.ExpiresOn, result.Value.RefreshTokenExpiration);

            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidCredentials_ReturnsError()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "hashedPassword");
            var errorMessage = "Email or password is incorrect!";

            _identityServiceMock.Setup(x => x.GetUserAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidCredentials])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InvalidCredentials, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());

            _tokenProviderMock.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.GetActiveRefreshTokenAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_EmailNotConfirmed_ReturnsUnauthorized()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "hashedPassword");
            var user = new User { Id = 1, Email = command.Email, DeletedAt = null };
            var errorMessage = "Please confirm your email first!";

            _identityServiceMock.Setup(x => x.GetUserAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.ShouldConfirmEmail])
                .Returns(new LocalizedString(LocalizationKeys.Auth.ShouldConfirmEmail, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());

            _tokenProviderMock.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.GetActiveRefreshTokenAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_UserDeleted_ReturnsUnauthorized()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "hashedPassword");
            var user = new User { Id = 1, Email = command.Email, DeletedAt = DateTime.UtcNow };
            var errorMessage = "User account is inactive!";

            _identityServiceMock.Setup(x => x.GetUserAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(user, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InactiveUser])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InactiveUser, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());

            _tokenProviderMock.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.GetActiveRefreshTokenAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}