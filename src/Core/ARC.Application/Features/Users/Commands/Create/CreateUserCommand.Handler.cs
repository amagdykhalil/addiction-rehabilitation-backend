using ARC.Application.Abstractions.Services;

namespace ARC.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
    {
        private readonly IIdentityService _identityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IStringLocalizer<CreateUserCommandHandler> _localizer;
        private readonly IUserActionLinkBuilder _userActionLinkBuilder;
        private readonly IUserEmailService _userEmailService;

        public CreateUserCommandHandler(
            IIdentityService identityService,
            IUnitOfWork unitOfWork,
            ILogger<CreateUserCommandHandler> logger,
            IUserActionLinkBuilder userActionLinkBuilder,
            IStringLocalizer<CreateUserCommandHandler> localizer,
            IUserEmailService userEmailService)
        {
            _identityService = identityService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _localizer = localizer;
            _userEmailService = userEmailService;
            _userActionLinkBuilder = userActionLinkBuilder;
        }

        public async Task<Result<int>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var user = command.MapToUser();
            // Create user with password using Identity service
            var identityResult = await _identityService.CreateUserAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Assign roles if provided
            if (command.Roles != null && command.Roles.Count > 0)
            {
                await _identityService.AddUserRolesAsync(user.Id, command.Roles, cancellationToken);
            }

            if (!identityResult.Succeeded)
            {
                return Result.Error(_localizer[LocalizationKeys.User.CreationFail]);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var confirmationLink = await _userActionLinkBuilder.BuildEmailConfirmationLinkAsync(user, cancellationToken);
            await _userEmailService.SendWelcomeAndConfirmationAsync(user, user.Email, confirmationLink);

            _logger.LogInformation("Created new user with ID: {UserId}", user.Id);
            return Result.Success(user.Id);
        }
    }
}