using ARC.Application.Features.Auth.Commands.ResetPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ARC.Application.Tests.Features.Auth.Commands
{
    public class ResetPasswordCommandHandlerTests : IClassFixture<LocalizationKeyFixture>
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IStringLocalizer<ResetPasswordCommandHandler>> _localizerMock;
        private readonly ResetPasswordCommandHandler _handler;
        private readonly User _user;

        public ResetPasswordCommandHandlerTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _localizerMock = new Mock<IStringLocalizer<ResetPasswordCommandHandler>>();
            _handler = new ResetPasswordCommandHandler(
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
        public async Task Handle_ValidResetCode_ReturnsSuccess()
        {
            // Arrange
            var code = "reset-code";
            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var command = new ResetPasswordCommand(_user.Email, encodedCode, "NewPassword123!");

            _identityServiceMock.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync(_user);
            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(_user))
                .ReturnsAsync(true);
            _identityServiceMock.Setup(x => x.ResetPasswordAsync(_user, code, command.NewPassword))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_UserNotFoundOrNotConfirmed_ReturnsError()
        {
            // Arrange
            var command = new ResetPasswordCommand("notfound@example.com", "code", "NewPassword123!");
            var errorMessage = "Invalid or expired reset code";

            _identityServiceMock.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidResetCode])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InvalidResetCode, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());
        }

        [Fact]
        public async Task Handle_InvalidCodeFormat_ReturnsError()
        {
            // Arrange
            var command = new ResetPasswordCommand(_user.Email, "invalid-format", "NewPassword123!");
            var errorMessage = "Invalid or expired reset code";

            _identityServiceMock.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync(_user);
            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(_user))
                .ReturnsAsync(true);

            _localizerMock.Setup(x => x[LocalizationKeys.Auth.InvalidResetCode])
                .Returns(new LocalizedString(LocalizationKeys.Auth.InvalidResetCode, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());
        }

        [Fact]
        public async Task Handle_ResetFails_ReturnsError()
        {
            // Arrange
            var code = "reset-code";
            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var command = new ResetPasswordCommand(_user.Email, encodedCode, "NewPassword123!");
            var failedResult = IdentityResult.Failed(new IdentityError { Description = "Invalid code" });

            _identityServiceMock.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync(_user);
            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(_user))
                .ReturnsAsync(true);
            _identityServiceMock.Setup(x => x.ResetPasswordAsync(_user, code, command.NewPassword))
                .ReturnsAsync(failedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid code", result.Errors.First());
        }
    }
}