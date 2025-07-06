namespace ARC.Application.Features.Roles.Commands.AddRoles
{
    public class AddUserRolesCommandHandler : ICommandHandler<AddRolesCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AddUserRolesCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AddUserRolesCommandHandler> _localizer;

        public AddUserRolesCommandHandler(
            IIdentityService identityService,
            ILogger<AddUserRolesCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IStringLocalizer<AddUserRolesCommandHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(AddRolesCommand command, CancellationToken cancellationToken)
        {
            await _identityService.AddRolesAsync(command.Roles, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Added roles : {Roles}", string.Join(",", command.Roles));
            return Result.Success(true);
        }
    }
}