namespace ARC.Application.Features.Users.Queries.ExistsById
{
    public class UserExistsByIdQueryHandler : IQueryHandler<UserExistsByIdQuery, bool>
    {
        private readonly IIdentityService _identityService;
        public UserExistsByIdQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Result<bool>> Handle(UserExistsByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByIdAsync(query.Id,cancellationToken);
            return Result.Success(user != null);
        }
    }
}