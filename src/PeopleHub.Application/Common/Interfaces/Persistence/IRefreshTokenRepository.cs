using PeopleHub.Domain.Aggregates.User;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IRefreshTokenRepository
{
    Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default);
}