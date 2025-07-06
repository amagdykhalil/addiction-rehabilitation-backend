using ARC.Application.Abstractions.Services;

namespace ARC.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler(
        IIdentityService identityService,
        IUnitOfWork unitOfWork,
        IUserEmailService userEmailService,
        IUserActionLinkBuilder userActionLinkBuilder,
        IStringLocalizer<ChangePasswordCommandHandler> localizer)
        : ICommandHandler<ChangePasswordCommand>
    {
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdIncludePersonAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Error(localizer[LocalizationKeys.User.NotFoundById]);
            }

            var passwordValid = await identityService.CheckPasswordAsync(user.Email, request.OldPassword, cancellationToken);
            if (!passwordValid)
            {
                return Result.Error(localizer[LocalizationKeys.User.InvalidEmailOrPassword]);
            }

            var result = await identityService.ChangePasswordAsync(user, request.OldPassword, request.NewPassword, cancellationToken);

            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault()?.Description ?? localizer[LocalizationKeys.User.UpdateFail];
                return Result.Error(error);
            }

            var resetLink = await userActionLinkBuilder.BuildPasswordResetLinkAsync(user, user.Email, cancellationToken);
            await userEmailService.SendPasswordChangedNotificationAsync(user, user.Email, resetLink);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
