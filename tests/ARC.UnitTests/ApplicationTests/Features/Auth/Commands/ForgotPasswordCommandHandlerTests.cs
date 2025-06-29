using ARC.Application.Abstractions.Services;
using ARC.Application.Features.Auth.Commands.ForgotPassword;
using Microsoft.Extensions.Configuration;

namespace ARC.Application.Tests.Features.Auth.Commands
{
    public class ForgotPasswordCommandHandlerTests : IClassFixture<LocalizationKeyFixture>
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IUserEmailService> _userEmailServiceMock;
        private readonly Mock<IStringLocalizer<ForgotPasswordCommandHandler>> _localizerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ForgotPasswordCommandHandler _handler;
        private readonly User _user;

        public ForgotPasswordCommandHandlerTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _userEmailServiceMock = new Mock<IUserEmailService>();
            _localizerMock = new Mock<IStringLocalizer<ForgotPasswordCommandHandler>>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(x => x["Frontend:BaseUrl"]).Returns("https://localhost:5173");

            _handler = new ForgotPasswordCommandHandler(
                _identityServiceMock.Object,
                _userEmailServiceMock.Object,
                _localizerMock.Object,
                _configurationMock.Object
            );

            _user = new User
            {
                Id = 1,
                Email = "test@example.com",
                UserName = "test@example.com"
            };
        }

        [Fact]
        public async Task Handle_UserExistsAndEmailConfirmed_SendsResetLink_ReturnsSuccess()
        {
            // Arrange
            var command = new ForgotPasswordCommand(_user.Email);

            _identityServiceMock.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync(_user);
            _identityServiceMock.Setup(x => x.IsEmailConfirmedAsync(_user))
                .ReturnsAsync(true);
            _identityServiceMock.Setup(x => x.GeneratePasswordResetTokenAsync(_user))
                .ReturnsAsync("reset-token");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _userEmailServiceMock.Verify(x => x.SendPasswordResetLinkAsync(_user, command.Email, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotFoundOrNotConfirmed_DoesNotSendEmail_ReturnsSuccess()
        {
            // Arrange
            var command = new ForgotPasswordCommand("notfound@example.com");

            _identityServiceMock.Setup(x => x.FindByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _userEmailServiceMock.Verify(x => x.SendPasswordResetLinkAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}