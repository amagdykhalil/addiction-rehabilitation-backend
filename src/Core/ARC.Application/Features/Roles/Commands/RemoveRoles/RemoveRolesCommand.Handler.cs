namespace ARC.Application.Features.Users.Commands.RemoveRoles
{
    public class RemoveUserRolesCommandHandler : ICommandHandler<RemoveRolesCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<RemoveUserRolesCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<RemoveUserRolesCommandHandler> _localizer;

        public RemoveUserRolesCommandHandler(
            IIdentityService identityService,
            ILogger<RemoveUserRolesCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IStringLocalizer<RemoveUserRolesCommandHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(RemoveRolesCommand command, CancellationToken cancellationToken)
        {
            await _identityService.RemoveFromRolesAsync(command.RoleIds, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Removed roles : {RoleIds}", string.Join(",", command.RoleIds));
            return Result.Success(true);
        }
    }
}