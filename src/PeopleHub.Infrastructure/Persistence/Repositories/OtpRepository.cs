using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Otp;
using PeopleHub.Domain.Enums;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class OtpRepository : IOtpRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OtpRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        OtpCode otpCode,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.OtpCodes.AddAsync(
            otpCode,
            cancellationToken);
    }

    public async Task<OtpCode?> GetLatestAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.OtpCodes
            .Where(x =>
                x.UserId == userId &&
                x.Purpose == purpose)
            .OrderByDescending(x => x.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task UpdateAsync(
        OtpCode otpCode,
        CancellationToken cancellationToken = default)
    {
        _dbContext.OtpCodes.Update(otpCode);

        return Task.CompletedTask;
    }

    public async Task DeleteExpiredAsync(
        CancellationToken cancellationToken = default)
    {
        var expired = await _dbContext.OtpCodes
            .Where(x => x.ExpiresAtUtc < DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        if (expired.Count == 0)
        {
            return;
        }

        _dbContext.OtpCodes.RemoveRange(expired);
    }
}