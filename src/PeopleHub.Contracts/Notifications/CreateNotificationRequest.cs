namespace PeopleHub.Contracts.Notifications;

public sealed class CreateNotificationRequest
{
    public int Type { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}