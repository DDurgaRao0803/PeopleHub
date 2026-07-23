namespace PeopleHub.Application.Common.Interfaces.Realtime;

public interface IRealtimeNotifier
{
    Task SendLocationUpdateAsync(
        Guid providerProfileId,
        decimal latitude,
        decimal longitude,
        DateTime updatedOnUtc,
        CancellationToken cancellationToken = default);
}