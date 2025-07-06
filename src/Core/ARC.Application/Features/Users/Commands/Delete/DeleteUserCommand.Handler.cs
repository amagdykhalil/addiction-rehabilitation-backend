using ARC.Application.Abstractions.Persistence;

namespace ARC.Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IStringLocalizer<DeleteUserCommandHandler> _localizer;

        public DeleteUserCommandHandler(
            IIdentityService identityService,
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteUserCommandHandler> logger,
            IStringLocalizer<DeleteUserCommandHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByIdAsync(command.Id, cancellationToken);
            if (user == null)
            {
                return Result.Error(_localizer[LocalizationKeys.User.NotFoundById, command.Id]);
            }

            // Delete user from Identity
            var identityResult = await _identityService.DeleteUserAsync(user, cancellationToken);
            if (!identityResult.Succeeded)
            {
                return Result.Error(_localizer[LocalizationKeys.User.DeleteFail]);
            }

            _logger.LogInformation("Soft deleted (deactivatedUserS) user with ID: {UserId}", command.Id);
            return Result.Success(true);
        }
    }
}