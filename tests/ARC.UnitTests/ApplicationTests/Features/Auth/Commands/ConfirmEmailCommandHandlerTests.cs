using ARC.Application.Features.Auth.Commands.ConfirmEmail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ARC.Application.Tests.Features.Auth.Commands
{
    public class ConfirmEmailCommandHandlerTests : IClassFixture<LocalizationKeyFixture>
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IStringLocalizer<ConfirmEmailCommandHandler>> _localizerMock;
        private readonly ConfirmEmailCommandHandler _handler;
        private readonly User _user;

        public ConfirmEmailCommandHandlerTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _localizerMock = new Mock<IStringLocalizer<ConfirmEmailCommandHandler>>();
            _handler = new ConfirmEmailCommandHandler(
                _identityServiceMock.Object,
                _localizerMock.Object
            );

            _user = new User
            {
                Id = 1,
                Email = "test@example.com",
                UserName = "test@example.com"
            };
        }

        [Fact]
        public async Task Handle_ValidConfirmationCode_ReturnsSuccess()
        {
            // Arrange
            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("some-code"));
            var command = new ConfirmEmailCommand(1, encodedCode);

            var successResult = IdentityResult.Success;

            _identityServiceMock.Setup(x => x.FindByIdAsync(command.UserId.ToString()))
                .ReturnsAsync(_user);

            _identityServiceMock.Setup(x => x.ConfirmEmailAsync(_user, It.IsAny<string>()))
                .ReturnsAsync(successResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _identityServiceMock.Verify(x => x.ConfirmEmailAsync(_user, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsError()
        {
            // Arrange
            var command = new ConfirmEmailCommand(999, "valid-code");
            var errorMessage = "Invalid or expired token";

            _identityServiceMock.Setup(x => x.FindByIdAsync(command.UserId.ToString()))
                .ReturnsAsync((User?)null);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidToken])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InvalidToken, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());
            _identityServiceMock.Verify(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidCodeFormat_ReturnsError()
        {
            // Arrange
            var command = new ConfirmEmailCommand(1, "invalid-code-format");
            var errorMessage = "Invalid or expired token";

            _identityServiceMock.Setup(x => x.FindByIdAsync(command.UserId.ToString()))
                .ReturnsAsync(_user);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidToken])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InvalidToken, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());
            _identityServiceMock.Verify(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ConfirmationFails_ReturnsError()
        {
            // Arrange
            var command = new ConfirmEmailCommand(1, "valid-code");
            var errorMessage = "Invalid or expired token";
            var failedResult = IdentityResult.Failed(new IdentityError { Description = "Invalid code" });

            _identityServiceMock.Setup(x => x.FindByIdAsync(command.UserId.ToString()))
                .ReturnsAsync(_user);

            _identityServiceMock.Setup(x => x.ConfirmEmailAsync(_user, It.IsAny<string>()))
                .ReturnsAsync(failedResult);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidToken])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InvalidToken, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());
        }

        [Fact]
        public async Task Handle_ChangedEmail_UpdatesEmailAndUserName_ReturnsSuccess()
        {
            // Arrange
            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("some-code"));
            var changedEmail = "newemail@example.com";
            var command = new ConfirmEmailCommand(_user.Id, encodedCode, changedEmail);

            var successResult = IdentityResult.Success;

            _identityServiceMock.Setup(x => x.FindByIdAsync(command.UserId.ToString()))
                .ReturnsAsync(_user);
            _identityServiceMock.Setup(x => x.ChangeEmailAsync(_user, changedEmail, It.IsAny<string>()))
                .ReturnsAsync(successResult);
            _identityServiceMock.Setup(x => x.SetUserNameAsync(_user, changedEmail))
                .ReturnsAsync(successResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _identityServiceMock.Verify(x => x.ChangeEmailAsync(_user, changedEmail, It.IsAny<string>()), Times.Once);
            _identityServiceMock.Verify(x => x.SetUserNameAsync(_user, changedEmail), Times.Once);
        }
    }
}