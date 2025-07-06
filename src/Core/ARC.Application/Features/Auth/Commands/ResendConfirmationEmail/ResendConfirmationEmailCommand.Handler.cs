using ARC.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;

namespace ARC.Application.Features.Auth.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandHandler : ICommandHandler<ResendConfirmationEmailCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserEmailService _userEmailService;
        private readonly IStringLocalizer<ResendConfirmationEmailCommandHandler> _localizer;
        private readonly IUserActionLinkBuilder _userActionLinkBuilder;

        public ResendConfirmationEmailCommandHandler(
            IIdentityService identityService,
            IUserEmailService userEmailService,
            IUserActionLinkBuilder userActionLinkBuilder,
            IStringLocalizer<ResendConfirmationEmailCommandHandler> localizer,
            IConfiguration configuration)
        {
            _identityService = identityService;
            _userEmailService = userEmailService;
            _localizer = localizer;
            _userActionLinkBuilder = userActionLinkBuilder;
        }

        public async Task<Result> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.FindByEmailIncludePersonAsync(request.Email, cancellationToken);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Result.Success();
            }

            var confirmationLink = await _userActionLinkBuilder.BuildEmailConfirmationLinkAsync(user, cancellationToken);
            await _userEmailService.SendConfirmationLinkAsync(user, request.Email, confirmationLink);
            return Result.Success();
        }
    }
}