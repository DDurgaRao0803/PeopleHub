using PeopleHub.Domain.Aggregates.Notifications;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface INotificationRepository
{
    Task AddAsync(
        Notification notification,
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<Notification>> GetUserNotificationsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);


    Task<Notification?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);


    Task UpdateAsync(
        Notification notification,
        CancellationToken cancellationToken = default);
}