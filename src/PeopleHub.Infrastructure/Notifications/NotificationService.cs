using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Notifications;
using PeopleHub.Contracts.Notifications;
using PeopleHub.Domain.Aggregates.Notifications;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Infrastructure.Notifications;

public sealed class NotificationService
    : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;


    public NotificationService(
        INotificationRepository notificationRepository,
        IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }



    public async Task<NotificationResponse> CreateAsync(
        Guid userId,
        CreateNotificationRequest request,
        CancellationToken cancellationToken = default)
    {
        var notification =
            new Notification(
                userId,
                (NotificationType)request.Type,
                request.Title,
                request.Message);



        await _notificationRepository.AddAsync(
            notification,
            cancellationToken);



        await _unitOfWork.SaveChangesAsync(
            cancellationToken);



        return Map(notification);
    }



    public async Task<IReadOnlyList<NotificationResponse>>
        GetUserNotificationsAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
    {
        var notifications =
            await _notificationRepository
                .GetUserNotificationsAsync(
                    userId,
                    cancellationToken);



        return notifications
            .Select(Map)
            .ToList();
    }



    public async Task MarkAsReadAsync(
        Guid notificationId,
        CancellationToken cancellationToken = default)
    {
        var notification =
            await _notificationRepository
                .GetByIdAsync(
                    notificationId,
                    cancellationToken);



        if (notification is null)
        {
            throw new KeyNotFoundException(
                "Notification not found.");
        }



        notification.MarkAsRead();



        await _notificationRepository.UpdateAsync(
            notification,
            cancellationToken);



        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }



    private static NotificationResponse Map(
        Notification notification)
    {
        return new NotificationResponse
        {
            Id = notification.Id,

            UserId = notification.UserId,

            Type = (int)notification.Type,

            Title = notification.Title,

            Message = notification.Message,

            IsRead = notification.IsRead,

            CreatedOnUtc = notification.CreatedOnUtc,

            ReadOnUtc = notification.ReadOnUtc
        };
    }
}