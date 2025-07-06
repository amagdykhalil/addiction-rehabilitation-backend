namespace ARC.Application.Features.Users.Commands.Update
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IStringLocalizer<UpdateUserCommandHandler> _localizer;

        public UpdateUserCommandHandler(
            IIdentityService identityService,
            ILogger<UpdateUserCommandHandler> logger,
            IStringLocalizer<UpdateUserCommandHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            // Get user by ID
            var user = await _identityService.GetUserByIdIncludePersonAsync(command.Id, cancellationToken);
            if (user == null)
            {
                return Result.Error(_localizer[LocalizationKeys.User.NotFoundById, command.Id]);
            }

            // Update data
            command.MapToExistingUser(user);

            var identityResult = await _identityService.UpdateUserAsync(user, cancellationToken);

            if (!identityResult.Succeeded)
            {
                return Result.Error(_localizer[LocalizationKeys.User.UpdateFail]);
            }

            _logger.LogInformation("Updated user with ID: {UserId}", command.Id);
            return Result.Success(true);
        }
    }
}