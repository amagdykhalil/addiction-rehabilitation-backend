using ARC.Application.Abstractions.Services;
using ARC.Application.Features.Auth.Commands.ChangePassword;
using ARC.Tests.Common.DataGenerators;
using Microsoft.AspNetCore.Identity;

namespace ARC.Application.Tests.Features.Auth.Commands
{
    public class ChangePasswordCommandHandlerTests : IClassFixture<LocalizationKeyFixture>
    {
        private readonly Mock<IIdentityService> _identityServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserEmailService> _userEmailServiceMock;
        private readonly Mock<IUserActionLinkBuilder> _userActionLinkBuilderMock;
        private readonly Mock<IStringLocalizer<ChangePasswordCommandHandler>> _localizerMock;
        private readonly ChangePasswordCommandHandler _handler;
        private readonly User _user;

        public ChangePasswordCommandHandlerTests()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userEmailServiceMock = new Mock<IUserEmailService>();
            _userActionLinkBuilderMock = new Mock<IUserActionLinkBuilder>();
            _localizerMock = new Mock<IStringLocalizer<ChangePasswordCommandHandler>>();

            _handler = new ChangePasswordCommandHandler(
                _identityServiceMock.Object,
                _unitOfWorkMock.Object,
                _userEmailServiceMock.Object,
                _userActionLinkBuilderMock.Object,
                _localizerMock.Object
            );

            _user = TestDataGenerators.UserFaker().Generate();

            // Setup default localization
            _localizerMock.Setup(x => x[LocalizationKeys.User.NotFoundById])
                .Returns(new LocalizedString(LocalizationKeys.User.NotFoundById, "User not found"));

            _localizerMock.Setup(x => x[LocalizationKeys.User.InvalidEmailOrPassword])
                .Returns(new LocalizedString(LocalizationKeys.User.InvalidEmailOrPassword, "Invalid email or password"));

            _localizerMock.Setup(x => x[LocalizationKeys.User.UpdateFail])
                .Returns(new LocalizedString(LocalizationKeys.User.UpdateFail, "Update failed"));
        }

        [Fact]
        public async Task Handle_ValidChangePassword_ReturnsSuccess()
        {
            // Arrange
            var command = new ChangePasswordCommand(_user.Id, "OldPassword123!", "NewPassword123!");
            var resetLink = "https://example.com/reset-password";

            _identityServiceMock.Setup(x => x.GetUserByIdIncludePersonAsync(command.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user);

            _identityServiceMock.Setup(x => x.CheckPasswordAsync(_user.Email, command.OldPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _identityServiceMock.Setup(x => x.ChangePasswordAsync(_user, command.OldPassword, command.NewPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);

            _userActionLinkBuilderMock.Setup(x => x.BuildPasswordResetLinkAsync(_user, _user.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(resetLink);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _userEmailServiceMock.Verify(x => x.SendPasswordChangedNotificationAsync(_user, _user.Email, resetLink), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsError()
        {
            // Arrange
            var command = new ChangePasswordCommand(999, "OldPassword123!", "NewPassword123!");
            var errorMessage = "User not found";

            _identityServiceMock.Setup(x => x.GetUserByIdIncludePersonAsync(command.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            _localizerMock.Setup(x => x[LocalizationKeys.User.NotFoundById])
                .Returns(new LocalizedString(LocalizationKeys.User.NotFoundById, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());

            _identityServiceMock.Verify(x => x.CheckPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _identityServiceMock.Verify(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _userEmailServiceMock.Verify(x => x.SendPasswordChangedNotificationAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_InvalidOldPassword_ReturnsError()
        {
            // Arrange
            var command = new ChangePasswordCommand(_user.Id, "WrongPassword123!", "NewPassword123!");
            var errorMessage = "Invalid email or password";

            _identityServiceMock.Setup(x => x.GetUserByIdIncludePersonAsync(command.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user);

            _identityServiceMock.Setup(x => x.CheckPasswordAsync(_user.Email, command.OldPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _localizerMock.Setup(x => x[LocalizationKeys.User.InvalidEmailOrPassword])
                .Returns(new LocalizedString(LocalizationKeys.User.InvalidEmailOrPassword, errorMessage));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors.First());

            _identityServiceMock.Verify(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
            _userEmailServiceMock.Verify(x => x.SendPasswordChangedNotificationAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ChangePasswordFails_ReturnsError()
        {
            // Arrange
            var command = new ChangePasswordCommand(_user.Id, "OldPassword123!", "NewPassword123!");
            var failedResult = IdentityResult.Failed(new IdentityError { Description = "Password does not meet requirements" });

            _identityServiceMock.Setup(x => x.GetUserByIdIncludePersonAsync(command.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user);

            _identityServiceMock.Setup(x => x.CheckPasswordAsync(_user.Email, command.OldPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _identityServiceMock.Setup(x => x.ChangePasswordAsync(_user, command.OldPassword, command.NewPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(failedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Password does not meet requirements", result.Errors.First());

            _userEmailServiceMock.Verify(x => x.SendPasswordChangedNotificationAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }



        [Fact]
        public async Task Handle_ValidChangePassword_SendsNotificationEmail()
        {
            // Arrange
            var command = new ChangePasswordCommand(_user.Id, "OldPassword123!", "NewPassword123!");
            var resetLink = "https://example.com/reset-password";

            _identityServiceMock.Setup(x => x.GetUserByIdIncludePersonAsync(command.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user);

            _identityServiceMock.Setup(x => x.CheckPasswordAsync(_user.Email, command.OldPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _identityServiceMock.Setup(x => x.ChangePasswordAsync(_user, command.OldPassword, command.NewPassword, It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);

            _userActionLinkBuilderMock.Setup(x => x.BuildPasswordResetLinkAsync(_user, _user.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(resetLink);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _userActionLinkBuilderMock.Verify(x => x.BuildPasswordResetLinkAsync(_user, _user.Email, It.IsAny<CancellationToken>()), Times.Once);
            _userEmailServiceMock.Verify(x => x.SendPasswordChangedNotificationAsync(_user, _user.Email, resetLink), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}