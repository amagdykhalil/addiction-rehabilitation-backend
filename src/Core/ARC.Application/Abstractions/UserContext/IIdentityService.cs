using Microsoft.AspNetCore.Identity;

namespace ARC.Application.Abstractions.UserContext
{
    /// <summary>
    /// Service interface for handling user identity and authentication operations.
    /// </summary>
    public interface IIdentityService
    {
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<User?> GetUserAsync(string email, string password);
        Task<IList<string>> GetRolesAsync(int userId);
        Task AddToRoleAsync(int userId, string role);
        Task<IdentityResult> ValidatePasswordAsync(string password);
        Task<IdentityResult> CreateUserAsync(User user, string password);
    }
}


