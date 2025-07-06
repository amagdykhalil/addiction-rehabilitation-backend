namespace ARC.Application.Features.Users.Queries.ExistsByNationalId
{
    public class UserExistsByNationalIdQueryHandler : IQueryHandler<UserExistsByNationalIdQuery, int?>
    {
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<UserExistsByNationalIdQueryHandler> _localizer;

        public UserExistsByNationalIdQueryHandler(
            IIdentityService identityService,
            IStringLocalizer<UserExistsByNationalIdQueryHandler> localizer)
        {
            _identityService = identityService;
            _localizer = localizer;
        }
        public async Task<Result<int?>> Handle(UserExistsByNationalIdQuery query, CancellationToken cancellationToken)
        {
            var id = await _identityService.IsExistsByNationalIdNumber(query.NationalIdNumber,cancellationToken);

            if (id == null)
            {
                return Result.NotFound(_localizer[LocalizationKeys.User.NotFoundByNationalId, query.NationalIdNumber]);
            }

            return Result.Success(id);
        }
    }
}