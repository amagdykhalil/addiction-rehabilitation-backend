using ARC.Application.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace ARC.Persistence.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        private readonly int _refreshTokenLifetime;
        public RefreshTokenRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _refreshTokenLifetime = configuration.GetValue<int>("RefreshToken:ExpirationDays");
        }


        public async Task<RefreshToken?> GetActiveRefreshTokenAsync(int userId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.RevokedOn == null && r.ExpiresOn > DateTime.UtcNow && r.UserId == userId);
        }

        public RefreshToken GenerateRefreshToken(int userId)
        {
            var randomNumber = new byte[32];
            string Token;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                Token = Convert.ToBase64String(randomNumber);
            }

            return new RefreshToken
            {
                Token = Token,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenLifetime),
                UserId = userId
            };
        }

        public async Task<RefreshToken?> GetWithUserAsync(string token)
        {
            return await _context.RefreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task<RefreshToken?> GetAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}



