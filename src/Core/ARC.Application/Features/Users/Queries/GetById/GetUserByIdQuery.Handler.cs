namespace ARC.Application.Features.Users.Queries.GetById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailsDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IStringLocalizer<GetUserByIdQueryHandler> _localizer;

        public GetUserByIdQueryHandler(IIdentityService identityService, IStringLocalizer<GetUserByIdQueryHandler> localizer)
        {
            _identityService = identityService;
            _localizer = localizer;
        }

        public async Task<Result<UserDetailsDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {

            var user = await _identityService.GetUserByIdIncludePersonAsync(query.Id, cancellationToken);

            if (user == null)
                return Result.Error(_localizer[LocalizationKeys.User.NotFoundById, query.Id]);

            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var roles = await _identityService.GetRolesAsync(user.Id, lang, cancellationToken);

            return Result.Success(user.ToDto(roles.ToList()));
        }
    }
}