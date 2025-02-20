using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext context) : EfRepositoryBase<RefreshToken, Guid, AppDbContext>(context), IRefreshTokenRepository
{
    public async Task<List<RefreshToken>> GetOldRefreshTokensAsync(Guid userId, int refreshTokenTtl)
    {
        List<RefreshToken> tokens = await Query()
            .AsNoTracking()
            .Where(r =>
                r.UserId == userId
                && r.RevokedDate == null
                && r.ExpiresAt >= DateTime.UtcNow
                && r.CreatedDate.AddDays(refreshTokenTtl) <= DateTime.UtcNow
            )
            .ToListAsync();

        return tokens;
    }
}
