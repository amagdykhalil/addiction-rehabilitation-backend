using ARC.Application.Features.Roles.Models;


namespace ARC.Application.Features.Users.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<GetAllRolesQueryHandler> _logger;
        private readonly IStringLocalizer<GetAllRolesQueryHandler> _localizer;

        public GetAllRolesQueryHandler(
            IIdentityService identityService,
            ILogger<GetAllRolesQueryHandler> logger,
            IStringLocalizer<GetAllRolesQueryHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<List<RoleDto>>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetAllRolesAsync(cancellationToken);
            
            _logger.LogInformation("Fetched all roles");
            return Result.Success(roles.ToRoleDtoList());
        }
    }
} 