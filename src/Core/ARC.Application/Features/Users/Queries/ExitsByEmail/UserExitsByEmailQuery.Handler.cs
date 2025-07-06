namespace ARC.Application.Features.Users.Queries.GetByEmail
{
    public class UserExitsByEmailQueryHandler : IQueryHandler<UserExitsByEmailQuery, int?>
    {
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<UserExitsByEmailQueryHandler> _localizer;

        public UserExitsByEmailQueryHandler(IIdentityService identityService, IStringLocalizer<UserExitsByEmailQueryHandler> localizer)
        {
            _identityService = identityService;
            _localizer = localizer;
        }

        public async Task<Result<int?>> Handle(UserExitsByEmailQuery query, CancellationToken cancellationToken)
        {
            var userId = await _identityService.IsExitsByEmail(query.Email,cancellationToken);

            if (userId == null)
                return Result.Error(_localizer[LocalizationKeys.User.NotFoundByEmail, query.Email]);

            return Result.Success(userId);
        }
    }
}