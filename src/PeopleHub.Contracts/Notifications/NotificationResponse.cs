namespace PeopleHub.Contracts.Notifications;

public sealed class NotificationResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Type { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ReadOnUtc { get; set; }
}