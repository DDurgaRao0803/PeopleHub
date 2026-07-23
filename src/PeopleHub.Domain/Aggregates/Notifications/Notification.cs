using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Notifications;

public sealed class Notification : AuditableEntity
{
    private Notification()
    {
    }


    public Notification(
    Guid userId,
    NotificationType type,
    string title,
    string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        UserId = userId;
        Type = type;
        Title = title.Trim();
        Message = message.Trim();

        IsRead = false;
    }


    public Guid UserId { get; private set; }


    public string Title { get; private set; } = string.Empty;


    public string Message { get; private set; } = string.Empty;


    public bool IsRead { get; private set; }


    public DateTime? ReadOnUtc { get; private set; }

    public NotificationType Type { get; private set; }



    public void MarkAsRead()
    {
        if (IsRead)
        {
            return;
        }

        IsRead = true;
        ReadOnUtc = DateTime.UtcNow;
    }
}