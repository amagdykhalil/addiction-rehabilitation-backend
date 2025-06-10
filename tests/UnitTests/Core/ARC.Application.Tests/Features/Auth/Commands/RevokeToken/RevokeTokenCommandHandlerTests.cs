using ARC.Application.Features.Auth.Commands.RevokeToken;

namespace ARC.API.Tests.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommandHandlerTests
    {
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly RevokeTokenCommandHandler _handler;

        public RevokeTokenCommandHandlerTests()
        {
            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _handler = new RevokeTokenCommandHandler(
                _refreshTokenRepositoryMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidActiveToken_RevokesTokenAndReturnsNoContent()
        {
            // Arrange
            var command = new RevokeTokenCommand("valid-token");
            var refreshToken = new Domain.Entities.RefreshToken
            {
                Token = command.Token,
                UserId = 1,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                RevokedOn = null
            };

            _refreshTokenRepositoryMock.Setup(x => x.GetAsync(command.Token))
                .ReturnsAsync(refreshToken);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(DateTime.UtcNow.Date, refreshToken.RevokedOn?.Date);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyToken_ReturnsNoContent()
        {
            // Arrange
            var command = new RevokeTokenCommand("");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _refreshTokenRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidOrInactiveToken_ReturnsNoContent()
        {
            // Arrange
            var command = new RevokeTokenCommand("invalid-token");

            _refreshTokenRepositoryMock.Setup(x => x.GetAsync(command.Token))
                .ReturnsAsync((Domain.Entities.RefreshToken?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_AlreadyRevokedToken_ReturnsNoContent()
        {
            // Arrange
            var command = new RevokeTokenCommand("revoked-token");
            var refreshToken = new Domain.Entities.RefreshToken
            {
                Token = command.Token,
                UserId = 1,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                RevokedOn = DateTime.UtcNow.AddDays(-1)
            };

            _refreshTokenRepositoryMock.Setup(x => x.GetAsync(command.Token))
                .ReturnsAsync(refreshToken);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}