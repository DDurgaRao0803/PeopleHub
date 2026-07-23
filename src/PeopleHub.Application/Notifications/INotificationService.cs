using PeopleHub.Contracts.Notifications;

namespace PeopleHub.Application.Notifications;

public interface INotificationService
{
    Task<NotificationResponse> CreateAsync(
        Guid userId,
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<NotificationResponse>> GetUserNotificationsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);


    Task MarkAsReadAsync(
        Guid notificationId,
        CancellationToken cancellationToken = default);
}