using ARC.Application.Features.Users.Queries.GetAllRoles;
using ARC.Application.Features.Users.Queries.Models;

namespace ARC.Application.Features.Users.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQueryHandler : IQueryHandler<GetAllUserRolesQuery, List<GetAllUserRolesQueryResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<GetAllUserRolesQueryHandler> _logger;
        private readonly IStringLocalizer<GetAllUserRolesQueryHandler> _localizer;

        public GetAllUserRolesQueryHandler(
            IIdentityService identityService,
            ILogger<GetAllUserRolesQueryHandler> logger,
            IStringLocalizer<GetAllUserRolesQueryHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<List<GetAllUserRolesQueryResponse>>> Handle(GetAllUserRolesQuery query, CancellationToken cancellationToken)
        {
            var roles = await _identityService.GetAllUserRolesAsync(query.UserId, cancellationToken);
            
            _logger.LogInformation("Fetched roles to user {UserId}", query.UserId);
            return Result.Success(roles.ToUserRoleQueryResponseList());
        }
    }
} 