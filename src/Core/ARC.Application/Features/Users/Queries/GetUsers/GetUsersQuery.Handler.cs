using ARC.Application.Common.Queries;
using ARC.Application.Features.Users.Queries.GetById;
using ARC.Application.Features.Users.Queries.Models;

namespace ARC.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IPaginatedQueryHandler<GetUsersQuery, UserDetailsDto>
    {
        private readonly IIdentityService _identityService;

        public GetUsersQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<Common.Models.PagedResult<UserDetailsDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var pagedUsers = await _identityService.GetUsersAsync(
                request.SearchQuery,
                request.Gender,
                request.NationalityId,
                request.RoleId,
                request.IsActive,
                request.SortBy,
                request.SortDirection,
                request.PageNumber,
                request.PageSize,
                lang,
                cancellationToken);

            var mapped = pagedUsers.Data.Select(uwr => uwr.ToUserDetailsDto()).ToList();
            var result = new Common.Models.PagedResult<UserDetailsDto>(mapped, pagedUsers.CurrentPage, pagedUsers.PageSize, pagedUsers.TotalCount);
            return Result.Success(result);
        }
    }
}
