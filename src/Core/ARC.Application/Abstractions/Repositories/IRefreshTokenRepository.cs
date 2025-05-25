using ARC.Domain.Entities;

namespace ARC.Application.Contracts.Persistence
{
    /// <summary>
    /// Repository interface for managing refresh tokens.
    /// </summary>
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>, IRepository
    {
        Task<RefreshToken?> GetActiveRefreshTokenAsync(int UserId);
        Task<RefreshToken?> GetAsync(string token);
        Task<RefreshToken?> GetWithUserAsync(string token);
        RefreshToken GenerateRefreshToken(int UserId);
    }
}



