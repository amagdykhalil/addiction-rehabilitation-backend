using ARC.Application.Abstractions.UserContext;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace ARC.Application.Features.Users.Commands.UpdateUserRoles
{
    public class UpdateUserRolesCommandHandler : ICommandHandler<UpdateUserRolesCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<UpdateUserRolesCommandHandler> _logger;
        private readonly IStringLocalizer<UpdateUserRolesCommandHandler> _localizer;

        public UpdateUserRolesCommandHandler(
            IIdentityService identityService,
            ILogger<UpdateUserRolesCommandHandler> logger,
            IStringLocalizer<UpdateUserRolesCommandHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<bool>> Handle(UpdateUserRolesCommand command, CancellationToken cancellationToken)
        {
            await _identityService.UpdateUserRolesAsync(command.UserId, command.RoleIds, cancellationToken);
            
            _logger.LogInformation("Added roles to user {UserId}: {RoleIds}", command.UserId, string.Join(",", command.RoleIds));
            return Result.Success(true);
        }
    }
} 