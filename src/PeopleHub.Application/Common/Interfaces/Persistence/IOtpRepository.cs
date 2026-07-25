using PeopleHub.Domain.Aggregates.Otp;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IOtpRepository
{
    Task AddAsync(
        OtpCode otpCode,
        CancellationToken cancellationToken = default);

    Task<OtpCode?> GetLatestAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        OtpCode otpCode,
        CancellationToken cancellationToken = default);

    Task DeleteExpiredAsync(
        CancellationToken cancellationToken = default);
}