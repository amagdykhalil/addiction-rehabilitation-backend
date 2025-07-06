using ARC.Application.Features.Roles.Models;

namespace ARC.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;
        private readonly IStringLocalizer<GetRoleByIdQueryHandler> _localizer;

        public GetRoleByIdQueryHandler(
            IIdentityService identityService,
            ILogger<GetRoleByIdQueryHandler> logger,
            IStringLocalizer<GetRoleByIdQueryHandler> localizer)
        {
            _identityService = identityService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery query, CancellationToken cancellationToken)
        {
            var role = await _identityService.GetRoleAsync(query.Id, cancellationToken);

            if (role is null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", query.Id);
                return Result.Error(_localizer[LocalizationKeys.Role.NotFound]);
            }

            return Result.Success(role.ToRoleDto());
        }
    }
}
