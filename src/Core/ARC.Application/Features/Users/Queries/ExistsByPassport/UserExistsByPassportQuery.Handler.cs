namespace ARC.Application.Features.Users.Queries.ExistsByPassport
{
    public class UserExistsByPassportQueryHandler : IQueryHandler<UserExistsByPassportQuery, int?>
    {
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<UserExistsByPassportQueryHandler> _localizer;
        public UserExistsByPassportQueryHandler(IIdentityService identityService,
        IStringLocalizer<UserExistsByPassportQueryHandler> localizer)
        {
            _identityService = identityService;
            _localizer = localizer;
        }
        public async Task<Result<int?>> Handle(UserExistsByPassportQuery query, CancellationToken cancellationToken)
        {
            var id = await _identityService.IsExistsByPasswordAsync(query.PassportNumber,cancellationToken);

            if (id == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.User.NotFoundByPassport, query.PassportNumber]);
            }

            return Result.Success(id);
        }
    }
}