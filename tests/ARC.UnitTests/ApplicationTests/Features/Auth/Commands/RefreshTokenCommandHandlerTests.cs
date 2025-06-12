using ARC.Application.Features.Auth.Commands.RefreshToken;


namespace ARC.API.Tests.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandlerTests
    {
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<ITokenProvider> _tokenProviderMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RefreshTokenCommandHandler _handler;

        public RefreshTokenCommandHandlerTests()
        {
            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _tokenProviderMock = new Mock<ITokenProvider>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new RefreshTokenCommandHandler(
                _refreshTokenRepositoryMock.Object,
                _identityServiceMock.Object,
                _tokenProviderMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRefreshToken_ReturnsNewAccessAndRefreshToken()
        {
            // Arrange
            var command = new RefreshTokenCommand("valid-refresh-token");
            var user = new User { Id = 1, Email = "test@example.com" };
            var existingRefreshToken = new Domain.Entities.RefreshToken
            {
                Token = command.Token,
                UserId = user.Id,
                User = user,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                RevokedOn = null
            };
            var newRefreshToken = new Domain.Entities.RefreshToken
            {
                Token = "new-refresh-token",
                UserId = user.Id,
                ExpiresOn = DateTime.UtcNow.AddDays(7)
            };
            var accessToken = "new-access-token";
            var tokenExpiration = DateTime.UtcNow.AddHours(1);

            _refreshTokenRepositoryMock.Setup(x => x.GetWithUserAsync(command.Token))
                .ReturnsAsync(existingRefreshToken);

            _refreshTokenRepositoryMock.Setup(x => x.GenerateRefreshToken(user.Id))
                .Returns(newRefreshToken);

            _tokenProviderMock.Setup(x => x.Create(user))
                .ReturnsAsync(accessToken);

            _tokenProviderMock.Setup(x => x.GetAccessTokenExpiration())
                .Returns(tokenExpiration);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(accessToken, result.Value.AccessToken);
            Assert.Equal(user.Id, result.Value.UserId);
            Assert.Equal(tokenExpiration, result.Value.ExpiresOn);
            Assert.Equal(newRefreshToken.Token, result.Value.RefreshToken);
            Assert.Equal(newRefreshToken.ExpiresOn, result.Value.RefreshTokenExpiration);

            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.RefreshToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyToken_ReturnsError()
        {
            // Arrange
            var command = new RefreshTokenCommand("");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid token", result.Errors.First());

            _refreshTokenRepositoryMock.Verify(x => x.GetWithUserAsync(It.IsAny<string>()), Times.Never);
            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.RefreshToken>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidOrInactiveToken_ReturnsError()
        {
            // Arrange
            var command = new RefreshTokenCommand("invalid-token");

            _refreshTokenRepositoryMock.Setup(x => x.GetWithUserAsync(command.Token))
                .ReturnsAsync((Domain.Entities.RefreshToken?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid token", result.Errors.First());

            _refreshTokenRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.RefreshToken>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}