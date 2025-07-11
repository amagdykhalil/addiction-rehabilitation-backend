using ARC.Application.Abstractions.Services;

namespace ARC.Application.Features.Auth.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : ICommandHandler<ChangeEmailCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserEmailService _userEmailService;
        private readonly IUserActionLinkBuilder _userActionLinkBuilder;
        private readonly IStringLocalizer<ChangeEmailCommandHandler> _localizer;

        public ChangeEmailCommandHandler(
            IIdentityService identityService,
            IUserEmailService userEmailService,
            IUserActionLinkBuilder userActionLinkBuilder,
            IStringLocalizer<ChangeEmailCommandHandler> localizer)
        {
            _identityService = identityService;
            _userEmailService = userEmailService;
            _userActionLinkBuilder = userActionLinkBuilder;
            _localizer = localizer;
        }

        public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            // Check if new email is already used
            var existingUser = await _identityService.IsExitsByEmail(request.NewEmail, cancellationToken);
            if (existingUser != null)
            {
                // For security, do not reveal if the email is in use
                return Result.Success();
            }

            // Find current user
            var user = await _identityService.GetUserByIdIncludePersonAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return Result.Success();
            }

            // Generate change email confirmation link
            var changeEmailLink = await _userActionLinkBuilder.BuildChangeEmailConfirmationLinkAsync(user, request.NewEmail, cancellationToken);
            await _userEmailService.SendConfirmationLinkAsync(user, request.NewEmail, changeEmailLink);

            return Result.Success();
        }
    }
}