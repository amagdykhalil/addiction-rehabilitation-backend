using ARC.Shared.Keys;
using System.Security.Claims;

namespace Infrastructure.Authentication;

/// <summary>
/// Extension methods for working with ClaimsPrincipal objects.
/// </summary>
public static class ClaimsPrincipalExtensions
{


    /// <summary>
    /// Gets the user ID from the claims principal.
    /// </summary>
    /// <param name="principal">The claims principal to extract the user ID from.</param>
    /// <param name="validationLocalizer">The localizer to use for validation messages.</param>
    /// <returns>The user ID as an integer.</returns>
    /// <exception cref="ApplicationException">Thrown when the user ID is not available in the claims.</exception>
    public static int GetUserId(this ClaimsPrincipal? principal, IStringLocalizer validationLocalizer)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(userId, out int parsedUserId) ?
            parsedUserId :
            throw new ApplicationException(validationLocalizer[LocalizationKeys.Validation.UserUnauthorized]);
    }
}
