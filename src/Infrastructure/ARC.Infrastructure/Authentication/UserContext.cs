using ARC.Application.Abstractions.UserContext;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

/// <summary>
/// Implementation of IUserContext that provides access to the current user's information.
/// </summary>
public sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<UserContext> _validationLocalizer;

    public UserContext(IHttpContextAccessor httpContextAccessor, IStringLocalizer<UserContext> validationLocalizer)
    {
        _httpContextAccessor = httpContextAccessor;
        _validationLocalizer = validationLocalizer;
    }

    public int UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId(_validationLocalizer) ??
        throw new ApplicationException("User context is unavailable");
}



